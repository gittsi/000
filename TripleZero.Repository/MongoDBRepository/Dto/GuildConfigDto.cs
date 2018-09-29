using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace TripleZero.Repository.Dto
{
    internal class GuildConfigDto
    {
        public Nullable<ObjectId> Id { get; set; }
        public string Name { get; set; }
        public List<string> Aliases { get; set; }
        public int SWGoHId { get; set; }
        public string SWGoHUrl { get; set; }
    }

}
