
using E_Commerce.Infrastructure.Operations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Services
{
    public class FileService 
    {
        readonly private IWebHostEnvironment _webHostEnvironment;
        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }


        async Task<string> FileRenameAsync(string path, IFormFile file, CancellationToken cancellationToken)
        {
            string newFileName = await Task.Run<string>(async () =>
           {
               string newFile = NameOperation.CharacterRegulatory
               (Path.GetFileNameWithoutExtension(file.FileName) + "-") 
               + DateTime.UtcNow.ToString("ddMMyyyyHHmmsss") 
                + Path.GetExtension(file.FileName);

               if (File.Exists($"{path}\\{newFile}"))
                   await FileRenameAsync(path, file, cancellationToken);

               return newFile;
           });

            return newFileName;
        }

    }
}
