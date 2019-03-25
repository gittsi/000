using System;
using System.Collections.Generic;
using System.Linq;

namespace SWGoH.Model
{
    public class Guild : Cache
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime SWGoHUpdateDate { get; set; }
        public DateTime EntryUpdateDate { get; set; }
        public int GalacticPower { get; set; }
        public int GalacticPowerAverage => GalacticPower / Players.Count();
        public List<Player> Players { get; set; }
        public override bool LoadedFromCache { get => base.LoadedFromCache; set => base.LoadedFromCache = value; }
    }
}
