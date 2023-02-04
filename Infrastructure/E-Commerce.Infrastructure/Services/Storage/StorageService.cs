using E_Commerce.Application.Abstractions.Storage;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Services.Storage
{
    public class StorageService : IStorageService
    {
        readonly IStorageService _storageService;

        public StorageService(IStorageService storageService)
        {
            _storageService = storageService;
        }

        public string StorageName => _storageService.GetType().Name;

        public Task DeleteAsync(string pathOrContainerName, string fileName)
        => _storageService.DeleteAsync(pathOrContainerName, fileName);  

        public List<string> GetFiles(string pathOrContainerName)
        => _storageService.GetFiles(pathOrContainerName);

        public bool HasFile(string pathOrContainerName, string fileName)
        => _storageService.HasFile(pathOrContainerName, fileName);

        public Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection files)
        => _storageService.UploadAsync(pathOrContainerName, files);
    }
}
