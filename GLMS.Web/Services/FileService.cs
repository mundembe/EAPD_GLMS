using Microsoft.AspNetCore.Http;

namespace GLMS.Web.Services
{
    public class FileService
    {
        public string UploadPdf(IFormFile file, string webRootPath)
        {
            if (file == null)
                throw new Exception("No file selected.");

            var extension = Path.GetExtension(file.FileName).ToLower();

            if (extension != ".pdf")
                throw new Exception("Only PDF files are allowed.");

            var fileName = $"{Guid.NewGuid()}.pdf";

            var folderPath = Path.Combine(webRootPath, "uploads", "contracts");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fullPath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return $"/uploads/contracts/{fileName}";
        }
    }
}