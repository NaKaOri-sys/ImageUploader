using ImageUploader.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ImageUploader.Controllers
{
    public class ImageUploadController : Controller
    {
        private readonly UploadOptions _options;

        public ImageUploadController(IOptions<UploadOptions> options)
        {
            _options = options.Value;
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile image)
        {
            try
            {
                if (image == null || image.Length == 0)
                    return BadRequest("No image found");
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                var imagePath = Path.Combine("/src/assets/imageUpload", fileName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
                var imageUrl = "/src/assets/imageUpload/" + fileName;

                _ = Task.Run(async () =>
                {
                    await Task.Delay(TimeSpan.FromMinutes(_options.TimeOutMinutes));

                    if (System.IO.File.Exists(imageUrl))
                    {
                        System.IO.File.Delete(imageUrl);
                    }
                });

                return Ok(imageUrl);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
