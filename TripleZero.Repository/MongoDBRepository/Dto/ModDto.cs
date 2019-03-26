﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using TripleZero.Repository.EnumDto;

namespace TripleZero.Repository.Repository.Dto
{
    public class ModDto
    {
        public string Name { get; set; }
        [DefaultValue(15)]
        [BsonDefaultValue(15)]
        [BsonElement("Lvl")]
        [JsonProperty("Lvl")]
        public int Level { get; set; }
        [JsonProperty("S")]
        [BsonElement("S")]
        [DefaultValue(5)]
        [BsonDefaultValue(5)]
        public int Star { get; set; }
        public ModSlot Type { get; set; }
        public string Rarity { get; set; }
        [BsonElement("PStat")]
        [JsonProperty("PStat")]
        public ModStatDto PrimaryStat { get; set; }
        [JsonProperty("SStat")]
        [BsonElement("SStat")]
        public List<ModStatDto> SecondaryStat { get; set; }
    }

    public class ModStatDto
    {
        [JsonProperty("VT")]
        [BsonElement("VT")]
        public ModValueType ValueType { get; set; }
        [JsonProperty("ST")]
        [BsonElement("ST")]
        public ModStatType StatType { get; set; }
        [JsonProperty("V")]
        [BsonElement("V")]
        public double Value { get; set; }
    }
}
