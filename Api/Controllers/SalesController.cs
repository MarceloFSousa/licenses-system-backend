using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Domain.Services;

[ApiController]
[Route("api/[controller]")]
public class SalesController : ControllerBase
{
    private readonly SaleService _service;

    public SalesController(SaleService service)
    {
        _service = service;
    }

    // GET: api/sales
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Sale>>> GetAll()
    {
        var sales = await _service.GetAllAsync();
        return Ok(sales);
    }

    // GET: api/sales/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Sale>> GetById(Guid id)
    {
        var sale = await _service.GetByIdAsync(id);
        if (sale == null) return NotFound();
        return Ok(sale);
    }

    // POST: api/sales
    [HttpPost]
    public async Task<ActionResult<Sale>> Create([FromBody] Sale sale)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var created = await _service.CreateAsync(sale);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PATCH: api/sales/{id}
    [HttpPatch("{id:guid}")]
    public async Task<ActionResult<Sale>> Patch(Guid id, [FromBody] SalePatchRequest sale)
    {
        var updated = await _service.UpdateAsync(sale, id);
        if (updated == null) return NotFound();
        return Ok(updated);
    }

    // DELETE: api/sales/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
