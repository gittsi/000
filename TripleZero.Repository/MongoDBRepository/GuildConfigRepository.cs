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
    public class GuildConfigRepository :  DocumentRepository<GuildConfig,GuildConfigDto>, IGuildConfigRepository 
    {
        MongoDBConnectionHelper _mongoDBConnectionHelper;
        IMapper _mapper;
        IMongoDatabase _db;

        public GuildConfigRepository(MongoDBConnectionHelper mongoDBConnectionHelper, IMapper mapper) : base(mongoDBConnectionHelper, mapper)
        {
            _mongoDBConnectionHelper = mongoDBConnectionHelper;
            _mapper = mapper;
            _db = _mongoDBConnectionHelper.GetMongoDbDatabase();
        }

        public string CollectionName => "Config.Guild";

        public async Task<List<GuildConfig>> GetAll()
        {
            return await new DocumentRepository<GuildConfig,GuildConfigDto>(_mongoDBConnectionHelper, _mapper).GetAll(this.CollectionName);
        }

        public async Task<GuildConfig> GetGuildConfigByAlias(string alias)
        {
            try
            {
                var filter = Builders<GuildConfigDto>.Filter.Eq("Aliases", alias);
                var result = await new DocumentRepository<GuildConfig, GuildConfigDto>(_mongoDBConnectionHelper, _mapper).GetByFilter(this.CollectionName, filter);
                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                
                throw;
            }
                 
        }

        public async Task<GuildConfig> GetGuildConfigById(string id)
        {
            var result = await new DocumentRepository<GuildConfig, GuildConfigDto>(_mongoDBConnectionHelper, _mapper).GetById(this.CollectionName, id);
            return result;            
        }
    }
}
