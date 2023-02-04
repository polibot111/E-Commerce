using E_Commerce.Application.Abstractions.Storage.Local;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Services.Storage.Local
{
    public class LocalStorage : ILocalStorage
    {

        readonly private IWebHostEnvironment _webHostEnvironment;

        public LocalStorage(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        async Task<bool> CopyFileAsync(string path, IFormFile file)
        {
            try
            {
                using FileStream fileStream = new(path, FileMode.Create);

                await fileStream.CopyToAsync(fileStream);
                await fileStream.FlushAsync();

                return true;
            }
            catch (Exception ex)
            {
                //todo log!
                throw ex;
            }

        }


        public async Task DeleteAsync(string pathOrContainerName, string fileName)
        => File.Delete(Path.Combine(pathOrContainerName, fileName));

        public List<string> GetFiles(string pathOrContainerName)
        {
           DirectoryInfo directoryInfo = new DirectoryInfo(pathOrContainerName);
            return directoryInfo.GetFiles().Select(f=> f.Name).ToList();
        }

        public bool HasFile(string pathOrContainerName, string fileName)
        => File.Exists(Path.Combine(pathOrContainerName, fileName));

        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string path, IFormFileCollection files)
        {
            try
            {
                string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);
                if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);

                List<(string, string)> datas = new();
                List<bool> results = new();
                foreach (IFormFile file in files)
                {

                    bool result = await CopyFileAsync(Path.Combine(path, file.FileName), file);
                    datas.Add((file.FileName, Path.Combine(path, file.FileName)));
                    results.Add(result);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


            //todo 
            return null;
        }
    }
}
