using CheckThingsAPI.Entities.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckThingsAPI.Services.Interfaces
{
    public interface IProductsService<T> where T : IDocument
    {
        List<T> GetAll();
        T GetById(string id);
        void Insert(T product);
        void Update(string id, T productToUp);
        void Delete(string id);
        string UploadFile(IFormFile file, string folderName, string pathToSave);
    }
}
