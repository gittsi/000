using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using SWGoH.Model;
using TripleZero.Repository.Dto;

namespace TripleZero.Repository.MongoDBRepository
{
    public class PlayerRepository : DocumentRepository<Player, PlayerDto>, IPlayerRepository
    {
        MongoDBConnectionHelper _mongoDBConnectionHelper;
        IMapper _mapper;
        IMongoDatabase _db;

        public PlayerRepository(MongoDBConnectionHelper mongoDBConnectionHelper, IMapper mapper) : base(mongoDBConnectionHelper, mapper)
        {
            _mongoDBConnectionHelper = mongoDBConnectionHelper;
            _mapper = mapper;
            _db = _mongoDBConnectionHelper.GetMongoDbDatabase();
        }

        public string CollectionName => "Player";
        private IMongoCollection<PlayerDto> _collection => _db.GetCollection<PlayerDto>(CollectionName);

        public async Task<List<Player>> GetAll()
        {
            return await new DocumentRepository<Player, PlayerDto>(_mongoDBConnectionHelper, _mapper).GetAll(this.CollectionName);
        }

        public async Task<Player> GetByAllyCode(string allyCode)
        {
            var filter = Builders<PlayerDto>.Filter.Eq("AllyCode", allyCode);
            var result = await new DocumentRepository<Player, PlayerDto>(_mongoDBConnectionHelper, _mapper).GetByFilter(this.CollectionName, filter);
            return result.FirstOrDefault();
        }

        public async Task<Player> GetByName(string name)
        {
            var filter = Builders<PlayerDto>.Filter.Eq("Name", name);
            var result = await new DocumentRepository<Player, PlayerDto>(_mongoDBConnectionHelper, _mapper).GetByFilter(this.CollectionName, filter);
            return result.FirstOrDefault();
        }

        public async Task<List<Player>> GetByGuild(string guildName)
        {
            var filter = Builders<PlayerDto>.Filter.Eq("GuildName", guildName);
            //var projection = Builders<PlayerDto>.Projection.Exclude(_=>_.Characters.Select(x=>x.Mods));
            var projection = Builders<PlayerDto>.Projection.Exclude("Characters.Mods");
            //var projection = Builders<PlayerDto>.Projection.Exclude("Mods");
            var result = await new DocumentRepository<Player, PlayerDto>(_mongoDBConnectionHelper, _mapper).GetByFilter(this.CollectionName, filter, projection);
            return result;
        }

        public async Task<bool> Upsert(Player player)
        {
            player.DBUpdateDate = DateTime.UtcNow;

            var playerDto = _mapper.Map<PlayerDto>(player);
            //var filter = Builders<GuildDto>.Filter.Eq("id", guildDto.Id);                
            //var updateQuery = Builders<GuildDto>.Update. //var options = Builders<QueueDto>.Sort.Descending(_ => _.Priority).Ascending(_ => _.NextRunDate);
            //var o = new FindOneAndUpdateOptions() { }
            var options = new UpdateOptions { IsUpsert = true };
            var result = await _collection.ReplaceOneAsync<PlayerDto>(doc => doc.AllyCode == playerDto.AllyCode, playerDto, options);

            var isUpdated = result.UpsertedId == null ? result.ModifiedCount > 0 : result.UpsertedId.AsString.Length > 0;

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
