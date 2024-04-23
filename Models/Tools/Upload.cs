using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;

namespace L2.Models.Tools
{
    public class Upload
    {
        private readonly IWebHostEnvironment en;
        public Upload(IWebHostEnvironment _en)
        {
            en = _en;
        }
        public async Task<string> Upload_Image(IFormFile Image, string type)
        {
            string FileExtension1 = Path.GetExtension(Image.FileName);
            string NewFileName = String.Concat(Guid.NewGuid().ToString(), FileExtension1);
            var path1 = $"{en.WebRootPath}\\{type}\\{NewFileName}";
            using (var stream = new FileStream(path1, FileMode.Create))
            {
                await Image.CopyToAsync(stream);
            }
            return NewFileName;
        }
        public async Task<string> Upload_Webp_Orginal(IFormFile img, string type)
        {
            using var image = await Image.LoadAsync(img.OpenReadStream());
            string originalPath = await SaveImage_Webp(image, image.Width,type);
            return originalPath;
        }
        public async Task<string> Upload_Webp_Full_Screen(IFormFile img, string type, int fullscreenWidth)
        {
            using var image = await Image.LoadAsync(img.OpenReadStream());
            string fullScreenPath = await SaveImage_Webp(image, fullscreenWidth,type);
            return fullScreenPath;
        }
        public async Task<string> Upload_Webp_Thumb(IFormFile img, string type, int thumbWidth)
        {
            using var image = await Image.LoadAsync(img.OpenReadStream());
            string thumPath = await SaveImage_Webp(image, thumbWidth,type);
            return thumPath;
        }
        private async Task<string> SaveImage_Webp(Image image, int width,string type )
        {
            if (width > 0)
            {
                int originalWidth = image.Width;
                int originalHeight = image.Height;

                image.Mutate(i => i.Resize(width, (width / originalWidth * originalHeight)));
            }
            string viewPath = $"{type}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", viewPath);
            string imageName = Guid.NewGuid().ToString() + ".webp";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            await image.SaveAsWebpAsync(Path.Combine(path, imageName), new WebpEncoder
            {
                Quality = 75
            });
            return "/" + viewPath.Replace("\\", "/") + "/" + imageName;
        }
    }
}