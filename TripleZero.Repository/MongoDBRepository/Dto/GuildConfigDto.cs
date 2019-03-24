using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TripleZero.Repository.Dto
{
    public class GuildConfigDto
    {      
        public string Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("aliases")]
        public List<string> Aliases { get; set; }
        [BsonElement("dAllyCode")]
        public string DPAllyCode { get; set; }
        [BsonElement("autoQueue")]
        public bool AutoQueue { get; set; }
        [BsonElement("retrieveEveryXMinutes")]
        public short RetrieveEveryXMinutes { get; set; }
    }

}
