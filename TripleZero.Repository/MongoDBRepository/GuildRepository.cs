using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Driver;
using SWGoH.Model;
using TripleZero.Repository.Dto;

namespace TripleZero.Repository.MongoDBRepository
{
    public class GuildRepository :  DocumentRepository<Guild,GuildDto>, IGuildRepository 
    {
        MongoDBConnectionHelper _mongoDBConnectionHelper;
        IMapper _mapper;
        IMongoDatabase _db;

        public GuildRepository(MongoDBConnectionHelper mongoDBConnectionHelper, IMapper mapper) : base(mongoDBConnectionHelper, mapper)
        {
            _mongoDBConnectionHelper = mongoDBConnectionHelper;
            _mapper = mapper;
            _db = _mongoDBConnectionHelper.GetMongoDbDatabase();
        }

        public string CollectionName => "Guild";
        private IMongoCollection<GuildDto> _collection => _db.GetCollection<GuildDto>(CollectionName);

        public async Task<List<Guild>> GetAll()
        {
            return await new DocumentRepository<Guild, GuildDto>(_mongoDBConnectionHelper, _mapper).GetAll(this.CollectionName);
        }

        public async Task<Guild> GetGuildByAlias(string alias)
        {
            var filter = Builders<GuildDto>.Filter.Eq("Aliases", alias);
            var result = await new DocumentRepository<Guild, GuildDto>(_mongoDBConnectionHelper, _mapper).GetByFilter(this.CollectionName,filter);
            return result.FirstOrDefault();            
        }

        public async Task<Guild> GetGuildByName(string name)
        {
            var filter = Builders<GuildDto>.Filter.Eq("Name", name);
            var result = await new DocumentRepository<Guild, GuildDto>(_mongoDBConnectionHelper, _mapper).GetByFilter(this.CollectionName, filter);
            return result.FirstOrDefault();
        }

        public async Task<Guild> GetGuildConfigById(string id)
        {
            var result = await new DocumentRepository<Guild, GuildDto>(_mongoDBConnectionHelper, _mapper).GetById(this.CollectionName, id);
            return result;            
        }

        public async Task<bool> GuildUpdate(Guild guild)
        {
            guild.EntryUpdateDate = DateTime.UtcNow;
            guild.Players.ForEach(p => p.DBUpdateDate = DateTime.UtcNow);

            var guildDto = _mapper.Map<GuildDto>(guild);
                //var filter = Builders<GuildDto>.Filter.Eq("id", guildDto.Id);                
                //var updateQuery = Builders<GuildDto>.Update. //var options = Builders<QueueDto>.Sort.Descending(_ => _.Priority).Ascending(_ => _.NextRunDate);
                //var o = new FindOneAndUpdateOptions() { }
                var options = new UpdateOptions { IsUpsert = true };
                var result = await _collection.ReplaceOneAsync(doc=>doc.Id == guildDto.Id, guildDto, options);

                var isUpdated = result.UpsertedId == null ? result.ModifiedCount>0 : result.UpsertedId.AsString.Length > 0;

                //if(isUpdated)
                //{
                //    guildDto.LastUpdated = DateTime.UtcNow.ToString();
                //    await _collection.ReplaceOneAsync<GuildDto>(doc => doc.Id == guildDto.Id, guildDto, options);
                //}

                //await _collection.InsertOneAsync(guildDto);
                return isUpdated;

        }
    }
}
