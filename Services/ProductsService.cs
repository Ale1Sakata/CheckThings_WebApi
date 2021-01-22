using CheckThingsAPI.Entities.Interfaces;
using CheckThingsAPI.Repository.Interfaces;
using CheckThingsAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CheckThingsAPI.Services
{
    public class ProductsService<T> : IProductsService<T>
        where T : IDocument
    {
        private readonly IProductsRepository<T> _productsRepository;

        public ProductsService(IProductsRepository<T> productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public List<T> GetAll()
        {
            Task<List<T>> products = _productsRepository.GetAllAsync();

            return products.Result;
        }

        public T GetById(string id)
        {
            Task<T> products = _productsRepository.GetByIdAsync(id);

            return products.Result;
        }

        public void Insert(T product)
        {
            _productsRepository.InsertOneAsync(product);
        }

        public void Update(string id, T productToUp)
        {
            _productsRepository.ReplaceOneAsync(id, productToUp);
        }

        public void Delete(string id)
        {
            _productsRepository.DeleteByIdAsync(id);
        }

        public string UploadFile(IFormFile file, string folderName, string pathToSave)
        {
            string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            string fullPath = Path.Combine(pathToSave, fileName);
            string dbPath = Path.Combine(folderName, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return dbPath;
        }
    }
}
