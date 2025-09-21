using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "http://auth:5004"; 
        options.Audience = "http://api:5005";
        options.RequireHttpsMetadata = false;
    });

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });
// Add HttpClient for the downstream API
builder.Services.AddHttpClient("DownstreamApi", client =>
{
    client.BaseAddress = new Uri("http://api:5005/");
});

var app = builder.Build();

// 2. Middleware
app.UseAuthentication();
app.UseAuthorization();

// 3. Map the BFF proxy route (place this after auth middleware)
// Public auth endpoints (bypass RequireAuthorization)
app.Map("/auth/{**catchAll}", async (HttpContext context, IHttpClientFactory httpClientFactory) =>
{
    var client = httpClientFactory.CreateClient("DownstreamApi");
    var downstreamRequest = new HttpRequestMessage(
        new HttpMethod(context.Request.Method),
        context.Request.Path + context.Request.QueryString);

    foreach (var header in context.Request.Headers)
        downstreamRequest.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());

    if (context.Request.ContentLength > 0)
        downstreamRequest.Content = new StreamContent(context.Request.Body);

    using var response = await client.SendAsync(downstreamRequest);

    context.Response.StatusCode = (int)response.StatusCode;
    foreach (var header in response.Headers)
        context.Response.Headers[header.Key] = header.Value.ToArray();
    foreach (var header in response.Content.Headers)
        context.Response.Headers[header.Key] = header.Value.ToArray();

    await response.Content.CopyToAsync(context.Response.Body);
});
// Secured API endpoints
app.Map("/api/{**catchAll}", async (HttpContext context, IHttpClientFactory httpClientFactory) =>
{
    var client = httpClientFactory.CreateClient("DownstreamApi");
    var downstreamRequest = new HttpRequestMessage(
        new HttpMethod(context.Request.Method),
        context.Request.Path + context.Request.QueryString);

    foreach (var header in context.Request.Headers)
        downstreamRequest.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());

    if (context.Request.ContentLength > 0)
        downstreamRequest.Content = new StreamContent(context.Request.Body);

    using var response = await client.SendAsync(downstreamRequest);

    context.Response.StatusCode = (int)response.StatusCode;
    foreach (var header in response.Headers)
        context.Response.Headers[header.Key] = header.Value.ToArray();
    foreach (var header in response.Content.Headers)
        context.Response.Headers[header.Key] = header.Value.ToArray();

    await response.Content.CopyToAsync(context.Response.Body);
}).RequireAuthorization();

// 4. Run the app
app.Run();