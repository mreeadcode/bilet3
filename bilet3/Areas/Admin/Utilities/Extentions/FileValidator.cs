using System.Threading.Tasks;
using bilet3.Areas.Admin.Utilities.Enums;
using Microsoft.Identity.Client;

namespace bilet3.Areas.Admin.Utilities.FileValidator
{
    public static class FileValidator
    {



        public static bool CheckFileType( this IFormFile file, string type)
        {
            if (file.ContentType.Contains(type))
            {
                return true;
            }
            return false;
        }

        public static bool CheckFileSize( this IFormFile file, int size, FileSizeType sizeType)
        {
            switch (sizeType)
            {
                case FileSizeType.KB:
                    return file.Length <= size * 1024;
                case FileSizeType.MB:
                    return file.Length <= size * 1024 * 1024;
                case FileSizeType.GB:
                    return file.Length <= size * 1024 * 1024 * 1024;
            }

            return false;
        }

        public static async Task<string> CraeteFile( this IFormFile file, params string[] roots)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetFileName(file.FileName);
            string filePath = string.Empty;

            foreach(var item in roots)
            {
                filePath = Path.Combine(filePath, item);
            }
            filePath = Path.Combine(filePath, fileName);

            using(FileStream fileStream = new(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return fileName;
        }

        public static void DeleteFile( this string fileName, params string[] roots)
        {

            string filePath = string.Empty;

            foreach (var item in roots)
            {
                filePath = Path.Combine(filePath, item);
            }
            filePath = Path.Combine(filePath, fileName);

            File.Delete(filePath);
        }
       

        
    }
}
