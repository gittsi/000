using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace TripleZero.Repository.Dto
{
    public partial class PlayerDto
    {
        [BsonIgnoreIfNull]
        public string Id { get; set; }
        [BsonIgnoreIfNull]
        public string GuildName { get; set; }        
        [BsonIgnoreIfNull]
        public string PlayerName { get; set; }
        [BsonIgnoreIfNull]
        public string AllyCode { get; set; }
        [BsonIgnoreIfNull]
        public string LastSWGoHUpdated { get; set; }
        [BsonIgnoreIfNull]
        public string LastUpdated { get; set; }
        //[BsonIgnoreIfDefault]
        //public int GPcharacters { get; set; }
        //[BsonIgnoreIfDefault]
        //public int GPships { get; set; }
        [BsonIgnoreIfNull]
        public List<CharacterDto> Characters { get; set; }
        [BsonIgnoreIfNull]
        public List<ShipDto> Ships { get; set; }
        [BsonIgnoreIfNull]
        public ArenaDto Arena { get; set; }
    }   
}