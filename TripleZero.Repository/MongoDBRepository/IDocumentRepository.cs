using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TripleZero.Repository.MongoDBRepository
{
    public interface IDocumentRepository<T,TDto>
    {
        Task<List<T>> GetAll(string collectionName);
        Task<T> GetById(string collectionName, string id);
        Task<List<T>> GetByFilter(string collectionName, MongoDB.Driver.FilterDefinition<TDto> filter);
    }
}
