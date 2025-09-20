using Api.Service;
using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpertsController : ControllerBase
    {
        private readonly ExpertsService _expertsService;
        private readonly LocalBucketService _bucket;
        private readonly ImageService _imageService;

        public ExpertsController(ExpertsService expertsService,LocalBucketService bucket,ImageService imageService)
        {
            _expertsService = expertsService;
            _bucket = bucket;
            _imageService = imageService;
        }

        // GET: api/Experts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expert>>> GetAll()
        {
            var experts = await _expertsService.GetAllAsync();
            return Ok(experts);
        }

        // GET: api/Experts/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Expert>> GetById(Guid id)
        {
            var expert = await _expertsService.GetByIdAsync(id);
            if (expert == null) return NotFound();
            return Ok(expert);
        }

        // POST: api/Experts
        [HttpPost]
        public async Task<ActionResult<Expert>> Create(
            [FromForm] string name,
        [FromForm] string description,
        [FromForm] DateTime initDate,
        [FromForm] IFormFile image,
        [FromForm] IFormFile fileContent)
        {
            var imageBytes = await _imageService.ConvertToBytesAsync(image);
            var imageExtension = await _imageService.GetExtension(image);
            var imgUrl = _bucket.UploadFile("images/expert_image", imageBytes, imageExtension);

            var fileContentBytes = await _imageService.ConvertToBytesAsync(fileContent);
            var fileContentExtension = await _imageService.GetExtension(fileContent);
            var fileContentUrl = _bucket.UploadFile("files/file_content", fileContentBytes, fileContentExtension);


            var created = await _expertsService.CreateAsync(name, description,initDate, imgUrl, fileContentUrl);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Expert>> Patch(Guid id, [FromBody] ExpertPutRequest expert)
        {
            var updated = await _expertsService.UpdateAsync(expert, id);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        // DELETE: api/Experts/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _expertsService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
