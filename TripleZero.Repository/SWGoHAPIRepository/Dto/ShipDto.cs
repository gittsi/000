﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TripleZero.Repository.SWGoHAPIRepository.Dto
{
    public class ShipDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("base_id")]
        public string NameId { get; set; }       
        [JsonProperty("alignment")]
        public string Alignment { get; set; }
    }
}
