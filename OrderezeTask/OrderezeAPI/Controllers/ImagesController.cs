using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderezeAPI.Controllers
{
    [ApiController]
    [Route("api/images")]
    public class ImagesController : ControllerBase
    {
        private readonly ILogger<ImagesController> _logger;
        private readonly IImageService _imageService;

        public ImagesController(ILogger<ImagesController> logger, IImageService imagesService)
        {
            _logger = logger;
            _imageService = imagesService;
        }

        /// <summary>
        /// Get all images
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <response code="200">Returns all images</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ImageModel>))]
        public async Task<ActionResult<List<ImageModel>>> GetAsync()
        {
            return Ok(await _imageService.GetImagesAsync());
        }

        /// <summary>
        /// Upload an image
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <response code="200">Returns the id of the uploaded image</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Interval Server Error</response>
        [HttpPost]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<ActionResult<int>> PostAsync([FromForm] string name, [FromForm] string description)
        {
            var formCollection = await Request.ReadFormAsync();
            if (string.IsNullOrWhiteSpace(name) || formCollection.Files.Count != 1 || !CheckFile(formCollection))
                return BadRequest();

            var result = await _imageService.AddNewImageAsync(name, description, formCollection.Files[0]);
            if (result != 0) return Ok(result);
            else return StatusCode(500);
        }

        private static bool CheckFile(IFormCollection formCollection)
        {
            var file = formCollection.Files[0];
            var AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
            var ext = file.FileName[file.FileName.LastIndexOf('.')..];
            var extension = ext.ToLower();

            var checkSize = file.Length > 0 && file.Length < 1024 * 1024 * 2;

            return checkSize && AllowedFileExtensions.Contains(extension);
        }

        /// <summary>
        /// Delete a specific image
        /// </summary>
        /// <remarks>
        /// Delete an image by a given id
        /// </remarks>
        /// <param name="id"></param>
        /// <response code="204">No Content</response>
        /// <response code="404">Not Found</response>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var result = await _imageService.DeleteImageAsync(id);

            if (result)
                return NoContent();

            return NotFound();
        }
    }
}
