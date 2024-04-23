
using Microsoft.AspNetCore.Hosting;

namespace WebCore.Tools
{
    public class Delete
    {
        private readonly  IWebHostEnvironment en;
        public Delete(IWebHostEnvironment _en)
        {
            en = _en;
        }
        public async Task<bool> Delete_Image(string Image)
        {
            string Path = $"{en.WebRootPath}{Image}";
            FileInfo file = new FileInfo(Path);
            if (file.Exists)
            {
                file.Delete();
                return true;
            }
            return false;
        }
    }
}