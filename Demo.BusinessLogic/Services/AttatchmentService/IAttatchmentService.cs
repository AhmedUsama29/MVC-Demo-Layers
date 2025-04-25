using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.AttatchmentService
{
    public interface IAttatchmentService
    {

        public string? Upload(IFormFile file , string FolderName);

        public bool Delete(string filePath);


    }
}
