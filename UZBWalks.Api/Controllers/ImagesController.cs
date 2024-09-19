using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UZBWalks.Api.Models.Domain;
using UZBWalks.Api.Models.DTO;
using UZBWalks.Api.Repositories;

namespace UZBWalks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto requestDto)
        {
            ValidateFileUpload(requestDto);

            if(ModelState.IsValid)
            {
                // Conver Dto To Domain Model
                var imageDomainModel = new Image
                {
                    File = requestDto.File,
                    FileExtension = Path.GetExtension(requestDto.File.FileName),
                    FileSizeInBytes = requestDto.File.Length,
                    FileName = requestDto.FileName,
                    FileDescription = requestDto.FileDescription
                };

                // User repository to upload image
                await _imageRepository.Upload(imageDomainModel);

                return Ok(imageDomainModel);
            }

            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadRequestDto imageUpload)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if(!allowedExtensions.Contains(Path.GetExtension(imageUpload.File.FileName)))
                ModelState.AddModelError("file", "Unsupported file extension");
            
            if (imageUpload.File.Length > 10485760)
                ModelState.AddModelError("file", "File size more than 10MB, please upload smaller size file");
        }
    }
}
