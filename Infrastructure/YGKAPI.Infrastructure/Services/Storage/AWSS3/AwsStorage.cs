using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YGKAPI.Application.Storage;
using YGKAPI.Application.Storage.AWSS3;
using YGKAPI.Application.Storage.Azure;

namespace YGKAPI.Infrastructure.Services.Storage.AWSS3
{
    public class AwsStorage : Storage, IAwsStorage
    {
        public Task DeleteAsync(string pathOrContainerName, string fileName)
        {
            throw new NotImplementedException("method.notImplementedException.message");
        }

        public List<string> GetFiles(string pathOrContainerName)
        {
            throw new NotImplementedException("method.notImplementedException.message");
        }

        public Task<(string fileName, string path)> UploadAsync(string pathOrContainerName, IFormFile files)
        {
            throw new NotImplementedException("method.notImplementedException.message");
        }

        public Task<List<(string fileName, string path)>> UploadRangeAsync(string pathOrContainerName, IFormFileCollection files)
        {
            throw new NotImplementedException("method.notImplementedException.message");
        }

        bool IStorage.HasFile(string pathOrContainerName, string fileName)
        {
            throw new NotImplementedException("method.notImplementedException.message");
        }
    }
}