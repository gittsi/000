using System;
using System.Collections.Generic;
using System.Text;

namespace SWGoH.Model
{
    public class Player : Cache
    {   
        public string Id { get; set; }
        public string GuildName { get; set; }
        public string PlayerName { get; set; }
        //public string PlayerNameInGame { get; set; }
        public string AllyCode { get; set; }
        public DateTime RosterUpdateDate { get; set; }
        public DateTime? DBUpdateDate { get; set; }
        //public int GalacticPowerCharacters { get; set; }
        //public int GalacticPowerShips { get; set; }
        public List<Character> Characters { get; set; }
        public List<Ship> Ships { get; set; }
        public Arena Arena { get; set; }
        public override bool LoadedFromCache { get => base.LoadedFromCache; set => base.LoadedFromCache = value; }
    }
}
