using CheckThingsAPI.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckThingsAPI.Repository.Interfaces
{
    public interface IProductsRepository<T> where T : IDocument
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(string id);
        Task InsertOneAsync(T document);
        void ReplaceOneAsync(string id, T document);
        Task DeleteByIdAsync(string id);
    }
}
