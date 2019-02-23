using System;
using System.Collections.Generic;
using System.Text;

namespace TripleZero.Bot.Model
{
    public class GroupYaz
    {
        public string GroupName { get; set; }
        public double TotalPointsAvailable { get; set; }   
        public List<Faction> Factions { get; set; }
    }

    public class Faction
    {
        public string FactionName { get; set; }
        public double TotalPointsAvailable { get; set; }
        public List<Yaz> ToonsInGroup { get; set; }
    }
}
