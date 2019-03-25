using System;
using System.Collections.Generic;
using System.Text;

namespace SWGoH.Model
{
    public class Character : Unit
    {
        public int Gear { get; set; }
        public double GP { get; set; }
        public List<Mod> Mods { get; set; }
        public List<Equipped> Equipped { get; set; }
        public List<Skill> Skills { get; set; }
    }

    public class Equipped
    {

        public string EquipmentId { get; set; }

        public long Slot { get; set; }
    }

    public partial class Skill
    {

        public long Tier { get; set; }


        public string Name { get; set; }


        public bool IsZeta { get; set; }

        public bool HasZeta => IsZeta && Tier == 8;


        public SkillType Type { get; set; }
    }

    public enum SkillType { Basic, Contract, Hardware, Leader, Special, Unique };
}

