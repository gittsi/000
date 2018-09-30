using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using SWGoH.Model;
using TripleZero.Repository.Dto;

namespace TripleZero.Repository.MongoDBRepository
{
    public class PlayerRepository : IPlayerRepository
    {
        MongoDBConnectionHelper _mongoDBConnectionHelper;
        IMapper _mapper;
        public PlayerRepository(MongoDBConnectionHelper mongoDBConnectionHelper, IMapper mapper)
        {
            _mongoDBConnectionHelper = mongoDBConnectionHelper;
            _mapper = mapper;
        }

        public string CollectionName => "Player";

        public async Task<Player> Get(string searchString)
        {
            try
            {
                var db = _mongoDBConnectionHelper.GetMongoDbDatabase();
                var collection = db.GetCollection<PlayerDto>(CollectionName);
                var result = collection.FindAsync<PlayerDto>(new BsonDocument()).Result;

                var playerDto = new PlayerDto();
                playerDto = result.FirstOrDefault();

                var player = _mapper.Map<Player>(playerDto);

                return player;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
