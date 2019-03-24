using System;
using System.Collections.Generic;
using System.Linq;
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
        private IMongoCollection<QueueDto> _collection => _db.GetCollection<QueueDto>(CollectionName);

        public Task<bool> ChangeQueueStatus(Queue queue)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Queue>> GetAll()
        {
            return await new DocumentRepository<Queue, QueueDto>(_mongoDBConnectionHelper, _mapper).GetAll(this.CollectionName);
        }

        public async Task<Queue> GetByItemId(string itemId)
        {
            var filter = Builders<QueueDto>.Filter.Eq("ItemId", itemId);
            var data = await new DocumentRepository<Queue, QueueDto>(_mongoDBConnectionHelper, _mapper).GetByFilter(this.CollectionName,filter);
            
            return data.FirstOrDefault();
        }

        public async Task<Queue> GetNextInQueue(string processingBy)
        {            
            var filter = Builders<QueueDto>.Filter.Eq("Status", 0);
            filter = filter & Builders<QueueDto>.Filter.Lt(_=> _.NextRunDate , DateTime.UtcNow.ToString());
            var updateQuery = Builders<QueueDto>.Update.Set(e => e.Status, EnumDto.QueueStatus.Processing).Set(e=>e.ProcessingBy, processingBy).Set(e=>e.ProcessingStartDate , DateTime.UtcNow.ToString() );
            //var options = Builders<QueueDto>.Sort.Descending(_ => _.Priority).Ascending(_ => _.NextRunDate);
            //var o = new FindOneAndUpdateOptions() { }
            var options = new FindOneAndUpdateOptions<QueueDto>();
            options.Sort = Builders<QueueDto>.Sort.Descending(_ => _.Priority).Ascending(_ => _.NextRunDate);
              //  .Descending(_ => _.Priority).Ascending(_ => _.NextRunDate);
            //options.Sort.Descending(_ => _.Priority).Ascending(_ => _.NextRunDate);

            //var dataDto = await collection.Find<QueueDto>(filter).Limit(1).SortByDescending(_ => _.Priority).ThenBy(_ => _.NextRunDate).FirstOrDefaultAsync();
            var dataDto = await _collection.FindOneAndUpdateAsync<QueueDto>(filter, updateQuery, options);

            var data = _mapper.Map<Queue>(dataDto);

            return data;
        }

        public async Task<bool> AddToQueue(Queue queue)
        {
            var queueDto = _mapper.Map<QueueDto>(queue);
            try
            {
                await _collection.InsertOneAsync(queueDto);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteFromQueue(Queue queue)
        {
            try
            {
                var result = await _collection.DeleteOneAsync(a => a.Id == queue.Id);
                return result.DeletedCount>0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task SetQueuesUnProcessed(IEnumerable<Queue> queues)
        {
            foreach (Queue queue in queues)
            {
                var filter = Builders<QueueDto>.Filter.Eq("_id", queue.Id);
                var update = Builders<QueueDto>.Update.Set("Status", 0).Set(_=>_.ProcessingStartDate, null).Set(_ => _.ProcessingBy, null);

                var result = await _collection.UpdateOneAsync(filter, update);

                var a = 1;
            } 
        }
    }
}
