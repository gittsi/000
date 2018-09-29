using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using SWGoH.Model;
using TripleZero.Repository.Dto;
using TripleZeroApi.Repository;

namespace TripleZero.Repository.MongoDBRepository
{
    public class GuildConfigRepository : IGuildConfigRepository
    {
        MongoDBConnectionHelper _mongoDBConnectionHelper;
        IMapper _mapper;
        public GuildConfigRepository(MongoDBConnectionHelper mongoDBConnectionHelper, IMapper mapper)
        {
            _mongoDBConnectionHelper = mongoDBConnectionHelper;
            _mapper = mapper;
        }

        public string CollectionName => "Config.Guild";

        public async Task<List<GuildConfig>> GetAll()
        {
            try
            {
                var db = _mongoDBConnectionHelper.GetMongoDbDatabase();
                var collection = db.GetCollection<GuildConfigDto>(CollectionName);
                var result = collection.FindAsync<GuildConfigDto>(new BsonDocument()).Result;
                
                var guildConfigDto = new List<GuildConfigDto>();
                while (await result.MoveNextAsync())
                {
                    guildConfigDto.AddRange(result.Current);
                }

                var guildConfig = _mapper.Map<List<GuildConfig>>(guildConfigDto);

                return guildConfig;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
