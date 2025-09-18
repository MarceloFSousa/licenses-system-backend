var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.WebHost.UseUrls("http://*:8080", "http://127.0.0.1:8081");

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapPost("/trades", async (HttpRequest request) =>
{
    using var reader = new StreamReader(request.Body);
    var body = await reader.ReadToEndAsync();

    Console.WriteLine("Received POST body:");
    Console.WriteLine(body);

    return Results.NotFound(new { message = "Received!", length = body.Length });
});

app.Run();
