using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

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
        public ActionResult<List<ImageModel>> Get()
        {
            return Ok(_imageService.GetImages());
        }

        /// <summary>
        /// Upload an image
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///        "name": "sampleImage",
        ///        "description": "My image",
        ///     }
        ///
        /// </remarks>
        /// <param name="imageModel"></param>
        /// <response code="200">Returns the id of the uploaded image</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public ActionResult<int> Post([FromBody] ImageModel imageModel)
        {
            return Ok(_imageService.AddNewImage(imageModel));
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
        public ActionResult Delete(int id)
        {
            var result = _imageService.DeleteImage(id);

            if (result)
                return NoContent();

            return NotFound();
        }
    }
}
