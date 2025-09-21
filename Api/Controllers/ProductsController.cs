using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Domain.Repositories;
using Domain.Services;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ProductService _service;
    private readonly ExpertsService _expertService;

    public ProductsController(ProductService service,ExpertsService expertService)
    {
        _service = service;
        _expertService = expertService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAll()
    {
        var products = await _service.GetAllAsync();
        return Ok(products);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Product>> GetById(Guid id)
    {
        var product = await _service.GetByIdAsync(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> Create([FromBody] ProductRequest product)
    {
        var expert = await _expertService.GetByIdAsync(product.ExpertId);

        var created = await _service.CreateAsync(product);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPatch("{id:guid}")]
    public async Task<ActionResult<Product>> Patch(Guid id, [FromBody] ProductPutRequest product)
    {

        var updated = await _service.UpdateAsync(product, id); // or use UpdateAsync if that’s your method name
        if (updated == null) return NotFound();

        return Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
