using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TripleZero.Repository.MongoDBRepository
{
    public class DocumentRepository<T, TDto> : IDocumentRepository<T, TDto>
    {
        MongoDBConnectionHelper _mongoDBConnectionHelper;
        IMapper _mapper;
        IMongoDatabase _db;
        public DocumentRepository(MongoDBConnectionHelper mongoDBConnectionHelper, IMapper mapper)
        {
            _mongoDBConnectionHelper = mongoDBConnectionHelper;
            _mapper = mapper;
            _db = _mongoDBConnectionHelper.GetMongoDbDatabase();
        }

        public async Task<List<T>> GetAll(string collectionName)
        {
            var collection = _db.GetCollection<T>(collectionName);
            var result = await collection.FindAsync<TDto>(new BsonDocument());

            var dataDto = new List<TDto>();
            while (await result.MoveNextAsync())
            {
                dataDto.AddRange(result.Current);
            }
            var data = _mapper.Map<List<T>>(dataDto);

            return data;
        }

        public async Task<List<T>> GetByFilter(string collectionName, FilterDefinition<TDto> filter, ProjectionDefinition<TDto> projection)
        {
            var collection = _db.GetCollection<TDto>(collectionName);
            var options = new FindOptions<TDto> { Projection = projection };

            var dataDto = await collection.FindAsync<TDto>(filter, options).Result.ToListAsync();
            var data = _mapper.Map<List<T>>(dataDto);

            return data;
        }

        public async Task<List<T>> GetByFilter(string collectionName, FilterDefinition<TDto> filter)
        {
            var collection = _db.GetCollection<TDto>(collectionName);
            var dataDto = await collection.FindAsync<TDto>(filter).Result.ToListAsync();
            var data = _mapper.Map<List<T>>(dataDto);

            return data;
        }

        public async Task<T> GetById(string collectionName, string id)
        {
            var filter = Builders<TDto>.Filter.Eq("_id", id);
            var data = await this.GetByFilter(collectionName, filter);

            return data.FirstOrDefault();
        }
    }
}
