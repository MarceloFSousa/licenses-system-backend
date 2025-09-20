using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Domain.Repositories;
using Domain.Services;

[ApiController]
[Route("api/[controller]")]
public class LicensesController : ControllerBase
{
    private readonly LicenseService _service;

    public LicensesController(LicenseService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<License>>> GetAll()
    {
        var licenses = await _service.GetAllAsync();
        return Ok(licenses);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<License>> GetById(Guid id)
    {
        var license = await _service.GetByIdAsync(id);
        if (license == null) return NotFound();
        return Ok(license);
    }

    [HttpPost]
    public async Task<ActionResult<License>> Create([FromBody] License license)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var created = await _service.CreateAsync(license);
        // Returns 201 Created with a Location header
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPatch("{id:guid}")]
    public async Task<ActionResult<License>> Put(Guid id, [FromBody] LicensePatchRequest license)
    {

        var updated = await _service.UpdateAsync(license, id); // Make sure you have UpdateFullAsync or reuse UpdateAsync for full updates
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
