using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace TripleZero.Repository.Dto
{
    public class GuildConfigDto
    {
        public Nullable<ObjectId> Id { get; set; }
        public string Name { get; set; }
        public List<string> Aliases { get; set; }
        public string DPAllyCode { get; set; }
        public bool AutoQueue { get; set; }
    }

}
