using Newtonsoft.Json;
using System.Collections.Generic;

namespace TripleZero.Repository.SWGoHHelpRepository.Dto
{
    public class GuildSWGoHHelp
    {
        [JsonProperty("roster")]
        public List<PlayerSWGoHHelp> Roster { get; set; }
        [JsonProperty("name")]
        public string GuildName { get; set; }
        [JsonProperty("gp")]
        public int GP { get; set; }
        [JsonProperty("updated")]
        public long Updated { get; set; }
    }
}
