﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YGKAPI.Application.Storage;

namespace YGKAPI.Infrastructure.Services.Storage
{
    public class StorageService : IStorageService
    {
        readonly IStorage _storage;

        public StorageService(IStorage storage)
        {
            _storage = storage;
        }
        public string StorageName { get => _storage.GetType().Name; }
        public async Task DeleteAsync(string pathOrContainerName, string fileName)
            => await _storage.DeleteAsync(pathOrContainerName, fileName);
        public List<string> GetFiles(string pathOrContainerName)
            => _storage.GetFiles(pathOrContainerName);
        public bool HasFile(string pathOrContainerName, string fileName)
            => _storage.HasFile(pathOrContainerName, fileName);
        public async Task<List<(string fileName, string path)>> UploadRangeAsync(string pathOrContainerName, IFormFileCollection files)
            => await _storage.UploadRangeAsync(pathOrContainerName, files);
        public async Task<(string fileName, string path)> UploadAsync(string pathOrContainerName, IFormFile files)
            => await _storage.UploadAsync(pathOrContainerName, files);

    }
}