using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.AttatchmentService
{
    public class AttatchmentService : IAttatchmentService
    {

        List<string> AllowedExtentions = new List<string> { ".jpg", ".png", ".jpeg" };

        int MaxSize = 2_097_152; //2MB

        public string? Upload(IFormFile file, string FolderName)
        {

            if (file is null) return null;

            var extension = Path.GetExtension(file.FileName);
            if (!AllowedExtentions.Contains(extension)) return null;

            if (file.Length >= MaxSize) return null;

             var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", FolderName);

            var fileName = $"{Guid.NewGuid()}_{file.FileName}";

            var filePath = Path.Combine(folderPath, fileName);

            using var fileStream = new FileStream(filePath, FileMode.Create);

            file.CopyTo(fileStream);

            return fileName;

        }

        public bool Delete(string filePath)
        {
            if (!File.Exists(filePath)) return false;
            else 
            { 
                File.Delete(filePath);
                return true;
            }
        }


    }
}
