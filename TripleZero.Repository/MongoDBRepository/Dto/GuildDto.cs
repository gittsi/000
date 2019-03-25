using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TripleZero.Repository.Dto
{
    public partial class GuildDto
    {
        [BsonIgnoreIfNull]
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastSWGoHUpdated { get; set; }
        public string LastUpdated { get; set; }
        [BsonIgnoreIfDefault]
        public int GP { get; set; }
        [BsonIgnoreIfDefault]
        public int GPaverage { get; set; }
        [BsonIgnoreIfNull]
        public List<string> PlayerNames { get; set; }
        [BsonIgnoreIfNull]
        public List<PlayerDto> Players { get; set; }
    }
}
