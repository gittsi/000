using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using SWGoH.Model;
using TripleZero.Repository.Dto;

namespace TripleZero.Repository.MongoDBRepository
{
    public class QueueRepository :  DocumentRepository<Queue, QueueDto>, IQueueRepository
    {
        MongoDBConnectionHelper _mongoDBConnectionHelper;
        IMapper _mapper;
        IMongoDatabase _db;
        public QueueRepository(MongoDBConnectionHelper mongoDBConnectionHelper, IMapper mapper) : base(mongoDBConnectionHelper, mapper)
        {
            _mongoDBConnectionHelper = mongoDBConnectionHelper;
            _mapper = mapper;
            _db = _mongoDBConnectionHelper.GetMongoDbDatabase();
        }

        public string CollectionName => "Queue";

        public Task<bool> ChangeQueueStatus(Queue queue)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Queue>> GetAll()
        {
            return await new DocumentRepository<Queue, QueueDto>(_mongoDBConnectionHelper, _mapper).GetAll(this.CollectionName);
        }

        public async Task<Queue> GetNextInQueue()
        {
            var collection = _db.GetCollection<QueueDto>(CollectionName);
            //var dataDto = await collection.Find<QueueDto>(new BsonDocument()).Limit(1).SortByDescending(_=>_.Priority).ThenBy(_=>_.NextRunDate).FirstOrDefaultAsync();
            var filter = Builders<QueueDto>.Filter.Eq("Status", 0);
            var updateQuery = Builders<QueueDto>.Update.Set(e => e.Status, EnumDto.QueueStatus.Processing);
            //var options = Builders<QueueDto>.Sort.Descending(_ => _.Priority).Ascending(_ => _.NextRunDate);
            //var o = new FindOneAndUpdateOptions() { }
            var options = new FindOneAndUpdateOptions<QueueDto>();
            options.Sort = Builders<QueueDto>.Sort.Descending(_ => _.Priority).Ascending(_ => _.NextRunDate);
              //  .Descending(_ => _.Priority).Ascending(_ => _.NextRunDate);
            //options.Sort.Descending(_ => _.Priority).Ascending(_ => _.NextRunDate);

            //var dataDto = await collection.Find<QueueDto>(filter).Limit(1).SortByDescending(_ => _.Priority).ThenBy(_ => _.NextRunDate).FirstOrDefaultAsync();
            var dataDto = await collection.FindOneAndUpdateAsync<QueueDto>(filter, updateQuery, options);

            var data = _mapper.Map<Queue>(dataDto);

            return data;
        }

        public async Task SetQueuesUnProcessed(IEnumerable<Queue> queues)
        {
            var collection = _db.GetCollection<QueueDto>(CollectionName);

            foreach (Queue queue in queues)
            {
                var filter = Builders<QueueDto>.Filter.Eq("_id", ObjectId.Parse(queue.Id.ToString()));
                var update = Builders<QueueDto>.Update.Set("Status", 0);

                var result = await collection.UpdateOneAsync(filter, update);

                var a = 1;
            } 
        }
    }
}
