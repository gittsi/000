using SWGoH.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TripleZero.Bot.Model;

namespace TripleZero.Bot.Helper
{
    public static class YazHelper
    {
        public static double GetTotalPoints => TotalPointsMandaToryToons + TotalPointLegacyToons + TotalPointBonusToons + TotalPointsZeta ;

        //Zeta
        public static double TotalPointsZeta = 340;

        //mandatory
        public static double TotalPointsMandaToryToons => GetMandatoryToons().Count * 10;

        public static double GetSithTotalPoints => GetMandatorySith().Count * 10;
        public static double GetSithEmpireTotalPoints => GetMandatorySithEmpire().Count * 10;
        public static double GetJediTotalPoints => GetMandatoryJedi().Count * 10;
        public static double GetResistanceTotalPoints => GetMandatoryResistance().Count * 10;
        public static double GetRebelsTotalPoints => GetMandatoryRebels().Count * 10;
        public static double GetBountyHuntersTotalPoints => GetMandatoryBountyHunters().Count * 10;
        public static double GetEmpireTotalPoints => GetMandatoryEmpire().Count * 10;
        public static double GetNightSistersTotalPoints => GetMandatoryNightSisters().Count * 10;
        public static double GetScoundrelsTotalPoints => GetMandatoryScoundrels().Count * 10;
        public static double GetOldRepublicTotalPoints => GetMandatoryOldRepublic().Count * 10;
        public static double GetOthersTotalPoints => GetMandatoryOthers().Count * 10;

        //Legacy
        public static double TotalPointLegacyToons => GetLegacyToons().Count * 3;

        //Bonus
        public static double TotalPointBonusToons => GetBonusToons().Count * 4;

        #region Mandatory Toons
        private static List<KeyValuePair<string, string>> GetMandatorySith()
        {
            var list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("Darth Traya","Sith"));
            list.Add(new KeyValuePair<string, string>("Darth Nihilus", "Sith"));
            list.Add(new KeyValuePair<string, string>("Darth Sion", "Sith"));            
            //list.Add(new KeyValuePair<string, string>("Count Dooku");
            return list;
        }

        private static List<KeyValuePair<string, string>> GetMandatorySithEmpire()
        {
            var list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("Darth Revan", "Sith Empire"));
            list.Add(new KeyValuePair<string, string>("HK-47", "Sith Empire"));
            list.Add(new KeyValuePair<string, string>("Bastila Shan (Fallen)", "Sith Empire"));
            list.Add(new KeyValuePair<string, string>("Sith Trooper", "Sith Empire"));
            //list.Add(new KeyValuePair<string, string>("Count Dooku");
            return list;
        }

        private static List<KeyValuePair<string, string>> GetMandatoryJedi()
        {
            var list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("Jedi Knight Revan", "Jedi"));
            list.Add(new KeyValuePair<string, string>("Jolee Bindo", "Jedi"));
            list.Add(new KeyValuePair<string, string>("Grand Master Yoda", "Jedi"));
            list.Add(new KeyValuePair<string, string>("General Kenobi", "Jedi"));
            list.Add(new KeyValuePair<string, string>("Hermit Yoda", "Jedi"));
            list.Add(new KeyValuePair<string, string>("Bastila Shan", "Jedi"));
            list.Add(new KeyValuePair<string, string>("Barriss Offee", "Jedi"));
            return list;
        }

        private static List<KeyValuePair<string, string>> GetMandatoryResistance()
        {
            var list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("Rey (Jedi Training)", "Resistance"));
            list.Add(new KeyValuePair<string, string>("R2-D2", "Resistance"));
            list.Add(new KeyValuePair<string, string>("BB-8", "Resistance"));
            //list.Add(new KeyValuePair<string, string>("Amilyn Holdo");
            return list;
        }

        private static List<KeyValuePair<string, string>> GetMandatoryRebels()
        {
            var list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("Commander Luke Skywalker", "Rebels"));
            list.Add(new KeyValuePair<string, string>("Han Solo", "Rebels"));
            list.Add(new KeyValuePair<string, string>("Chewbacca", "Rebels"));
            list.Add(new KeyValuePair<string, string>("Obi-Wan Kenobi (Old Ben)", "Rebels"));
            list.Add(new KeyValuePair<string, string>("C-3PO", "Rebels"));
            return list;
        }

        private static List<KeyValuePair<string, string>> GetMandatoryBountyHunters()
        {
            var list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("Bossk", "Bounty Hunters"));
            list.Add(new KeyValuePair<string, string>("Jango Fett", "Bounty Hunters"));
            list.Add(new KeyValuePair<string, string>("Dengar", "Bounty Hunters"));
            list.Add(new KeyValuePair<string, string>("Boba Fett", "Bounty Hunters"));
            return list;
        }

        private static List<KeyValuePair<string, string>> GetMandatoryEmpire()
        {
            var list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("Emperor Palpatine", "Empire"));
            list.Add(new KeyValuePair<string, string>("Darth Vader", "Empire"));
            list.Add(new KeyValuePair<string, string>("Grand Admiral Thrawn", "Empire"));
            list.Add(new KeyValuePair<string, string>("Shoretrooper", "Empire"));
            list.Add(new KeyValuePair<string, string>("Death Trooper", "Empire"));
            return list;
        }

        private static List<KeyValuePair<string, string>> GetMandatoryNightSisters()
        {
            var list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("Asajj Ventress", "NightSisters"));
            list.Add(new KeyValuePair<string, string>("Mother Talzin", "NightSisters"));
            list.Add(new KeyValuePair<string, string>("Old Daka", "NightSisters"));
            list.Add(new KeyValuePair<string, string>("Nightsister Spirit", "NightSisters"));
            list.Add(new KeyValuePair<string, string>("Nightsister Zombie", "NightSisters"));
            return list;
        }

        private static List<KeyValuePair<string, string>> GetMandatoryScoundrels()
        {
            var list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("Qi'ra", "Scoundrels"));
            list.Add(new KeyValuePair<string, string>("Vandor Chewbacca", "Scoundrels"));
            list.Add(new KeyValuePair<string, string>("L3-37", "Scoundrels"));
            list.Add(new KeyValuePair<string, string>("Enfys Nest", "Scoundrels"));
            return list;
        }

        private static List<KeyValuePair<string, string>> GetMandatoryOldRepublic()
        {
            var list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("Zaalbar", "Old Republic"));
            list.Add(new KeyValuePair<string, string>("Mission Vao", "Old Republic"));
            list.Add(new KeyValuePair<string, string>("Carth Onasi", "Old Republic"));
            list.Add(new KeyValuePair<string, string>("Canderous Ordo", "Old Republic"));
            return list;
        }

        private static List<KeyValuePair<string, string>> GetMandatoryOthers()
        {
            var list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("Wampa", "Rest Mandatory"));
            list.Add(new KeyValuePair<string, string>("General Grievous", "Rest Mandatory"));
            list.Add(new KeyValuePair<string, string>("B2 Super Battle Droid", "Rest Mandatory"));
            list.Add(new KeyValuePair<string, string>("CT-7567 \"Rex\"", "Rest Mandatory"));
            list.Add(new KeyValuePair<string, string>("Chirrut Îmwe", "Rest Mandatory"));
            list.Add(new KeyValuePair<string, string>("Baze Malbus", "Rest Mandatory"));
            return list;
        }

        private static List<KeyValuePair<string, string>> GetMandatoryToons()
        {
            var list = new List<KeyValuePair<string, string>>();
            //Sith
            list.AddRange(GetMandatorySith());

            //Sith Empire
            list.AddRange(GetMandatorySithEmpire());

            //Jedi
            list.AddRange(GetMandatoryJedi());

            //Resistance
            list.AddRange(GetMandatoryResistance());

            //Rebels
            list.AddRange(GetMandatoryRebels());

            //Bounty Hunters
            list.AddRange(GetMandatoryBountyHunters());

            //Empire
            list.AddRange(GetMandatoryEmpire());

            //NightSisters
            list.AddRange(GetMandatoryNightSisters());

            //Scoundrels
            list.AddRange(GetMandatoryScoundrels());

            //Old Republic
            list.AddRange(GetMandatoryOldRepublic());

            //Others
            list.AddRange(GetMandatoryOthers());



            //list.Add("Ezra Bridger");

            //list.Add("Magmatrooper");





            return list;
        }
        #endregion

        #region Legacy Toons
        private static List<KeyValuePair<string, string>> GetLegacyFirstOrder()
        {
            var list = new List<KeyValuePair<string, string>>();

            list.Add(new KeyValuePair<string, string>("Kylo Ren (Unmasked)", "First Order"));
            list.Add(new KeyValuePair<string, string>("Kylo Ren", "First Order"));
            list.Add(new KeyValuePair<string, string>("First Order Stormtrooper", "First Order"));
            list.Add(new KeyValuePair<string, string>("First Order Officer", "First Order"));
            list.Add(new KeyValuePair<string, string>("First Order Executioner", "First Order"));
            return list;
        }

        private static List<KeyValuePair<string, string>> GetLegacyImperials()
        {
            var list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("General Veers","Imperials"));
            list.Add(new KeyValuePair<string, string>("Colonel Starck", "Imperials"));
            list.Add(new KeyValuePair<string, string>("Snowtrooper", "Imperials"));
            //list.Add("Stormtrooper");
            list.Add(new KeyValuePair<string, string>("Range Trooper", "Imperials"));
            list.Add(new KeyValuePair<string, string>("Magmatrooper", "Imperials"));
            return list;
        }

        private static List<KeyValuePair<string, string>> GetLegacyPhoenix()
        {
            var list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("Ezra Bridger", "Phoenix"));
            list.Add(new KeyValuePair<string, string>("Hera Syndulla", "Phoenix"));
            list.Add(new KeyValuePair<string, string>("Kanan Jarrus", "Phoenix"));
            list.Add(new KeyValuePair<string, string>("Sabine Wren", "Phoenix"));
            list.Add(new KeyValuePair<string, string>("Garazeb \"Zeb\" Orrelios", "Phoenix"));
            return list;
        }

        private static List<KeyValuePair<string, string>> GetLegacyResistance()
        {
            var list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("Finn", "Resistance Legacy"));
            list.Add(new KeyValuePair<string, string>("Poe Dameron", "Resistance Legacy"));
            list.Add(new KeyValuePair<string, string>("Rey (Scavenger)", "Resistance Legacy"));
            list.Add(new KeyValuePair<string, string>("Resistance Pilot", "Resistance Legacy"));
            list.Add(new KeyValuePair<string, string>("Resistance Trooper", "Resistance Legacy"));
            return list;
        }

        private static List<KeyValuePair<string, string>> GetLegacyToons()
        {
            var list = new List<KeyValuePair<string, string>>();

            //FO
            list.AddRange(GetLegacyFirstOrder());

            //Imperials
            list.AddRange(GetLegacyImperials());

            //Phoenix
            list.AddRange(GetLegacyPhoenix());

            //Resistance
            list.AddRange(GetLegacyResistance());

            return list;
        }
        #endregion

        #region Bonus Toons
        private static List<KeyValuePair<string, string>> GetBonusClones()
        {
            var list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("CC-2224 \"Cody\"", "Clones"));
            list.Add(new KeyValuePair<string, string>("Clone Sergeant - Phase I", "Clones"));
            list.Add(new KeyValuePair<string, string>("CT-21-0408 \"Echo\"", "Clones"));
            list.Add(new KeyValuePair<string, string>("CT-5555 \"Fives\"", "Clones"));
            return list;
        }
        private static List<KeyValuePair<string, string>> GetBonusEwoks()
        {
            var list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("Chief Chirpa", "Ewoks"));
            list.Add(new KeyValuePair<string, string>("Wicket", "Ewoks"));
            list.Add(new KeyValuePair<string, string>("Ewok Elder", "Ewoks"));
            list.Add(new KeyValuePair<string, string>("Paploo", "Ewoks"));
            list.Add(new KeyValuePair<string, string>("Logray", "Ewoks"));

            return list;
        }
        private static List<KeyValuePair<string, string>> GetBonusJawa()
        {
            var list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("Chief Nebit", "Jawas"));
            list.Add(new KeyValuePair<string, string>("Jawa", "Jawas"));
            list.Add(new KeyValuePair<string, string>("Jawa Engineer", "Jawas"));
            list.Add(new KeyValuePair<string, string>("Java Scavenger", "Jawas"));
            list.Add(new KeyValuePair<string, string>("Dathcha", "Jawas"));

            return list;
        }
        private static List<KeyValuePair<string, string>> GetBonusOthers()
        {
            var list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("Mace Windu", "Rest Bonus"));
            list.Add(new KeyValuePair<string, string>("Count Dooku", "Rest Bonus"));

            return list;
        }

        private static List<KeyValuePair<string, string>> GetBonusToons()
        {
            var list = new List<KeyValuePair<string, string>>();

            //Clones
            list.AddRange(GetBonusClones());

            //Ewoks
            list.AddRange(GetBonusEwoks());

            //Jawa
            list.AddRange(GetBonusJawa());

            //Others
            list.AddRange(GetBonusOthers());

            return list;
        }

        #endregion

        //private static List<string> GetNormal()
        //{
        //    var list = new List<string>();

        //    list.Add("Visas Marr");

        //    list.Add("General Kenobi");


        //    list.Add("General Veers");
        //    list.Add("Colonel Starck");
        //    list.Add("Snowtrooper");
        //    list.Add("Stormtrooper");
        //    list.Add("Range Trooper");

        //    list.Add("Imperial Probe Droid");
        //    list.Add("Hera Syndulla");
        //    list.Add("Kanan Jarrus");
        //    list.Add("Sabine Wren");
        //    list.Add("Chopper");
        //    list.Add("Garazeb \"Zeb\" Orrelios");

        //    return list;
        //}

        //private static List<string> GetLow()
        //{
        //    var list = new List<string>();

        //    list.Add("Finn");
        //    list.Add("Poe Dameron");
        //    list.Add("Rey (Scavenger)");
        //    list.Add("Resistance Pilot");
        //    list.Add("Resistance Trooper");

        //    return list;
        //}

        private static List<string> GetYazZetaCount()
        {
            List<string> yazList = new List<string>();
            yazList.Add("Darth Traya");
            yazList.Add("Chewbacca");
            yazList.Add("Asajj Ventress");
            yazList.Add("Mother Talzin");
            yazList.Add("R2-D2");
            yazList.Add("Jedi Knight Revan");
            yazList.Add("Darth Revan");
            //yazList.Add("Bastila Shan (Fallen)");

            //yazList.Add("Kylo Ren (Unmasked)");
            return yazList;

        }

        private static List<string> GetYazZetaExists()
        {
            List<string> yazList = new List<string>();
            yazList.Add("Rey (Jedi Training)");
            yazList.Add("Emperor Palpatine");
            yazList.Add("Han Solo");
            yazList.Add("Bossk");
            yazList.Add("BB-8");
            yazList.Add("Darth Sion");
            yazList.Add("Commander Luke Skywalker");
            yazList.Add("Grand Master Yoda");
            yazList.Add("Vandor Chewbacca");
            yazList.Add("Enfys Nest");
            yazList.Add("Barriss Offee");
            yazList.Add("Hermit Yoda");
            yazList.Add("Jolee Bindo");
            yazList.Add("Zaalbar");
            yazList.Add("Mission Vao");
            yazList.Add("Carth Onasi");
            yazList.Add("Darth Nihilus");
            yazList.Add("HK-47");


            //yazList.Add("Bastila Shan");


            //yazList.Add("Bossk");

            //yazList.Add("General Veers");





            //yazList.Add("Kanan Jarrus");

            //yazList.Add("Visas Marr");
            //yazList.Add("Finn");
            //yazList.Add("Sabine Wren");

            //yazList.Add("Chief Chirpa");
            //yazList.Add("Wicket");
            //yazList.Add("Jedi Knight Revan");


            return yazList;
        }

        private static IEnumerable<Character> GetYazGearMandatory(Player player)
        {
            return player.Characters.Where(p => GetMandatoryToons().Exists(v => v.Key == p.Name));
        }

        private static IEnumerable<Character> GetYazGearLegacy(Player player)
        {
            return player.Characters.Where(p => GetLegacyToons().Exists(v=>v.Key == p.Name));
        }

        private static IEnumerable<Character> GetYazGearBonus(Player player)
        {
            return player.Characters.Where(p => GetBonusToons().Exists(v => v.Key == p.Name));
        }



        private static List<Yaz> GetCharsZetaCount(Player player)
        {
            List<Yaz> yazClass = new List<Yaz>();

            foreach (var unitName in GetYazZetaCount())
            {
                var unit = player.Characters.FirstOrDefault(p => p.Name == unitName);
                yazClass.Add(new Yaz() { ToonName = unit == null ? unitName : unit.Name, Score = unit == null ? 0 : unit.Skills.Count(p => p.HasZeta) * 10 });
            }
            return yazClass;
        }

        //public static List<GroupYaz> GetToonsBreakDown(Player player)
        //{
        //    List<GroupYaz> toonsBreakDown = new List<GroupYaz>();

        //    GroupYaz mandatoryToons = new GroupYaz()
        //    {
        //        GroupName = "Mandatory Toons"                
        //    };
        //    GroupYaz legacyToons = new GroupYaz()
        //    {
        //        GroupName = "Legacy Toons"
        //    };
        //    GroupYaz bonusToons = new GroupYaz()
        //    {
        //        GroupName = "Bonus Toons"
        //    };

        //    var mandatoryFactions = new List<Faction>();
        //    mandatoryFactions.Add(new Faction()
        //    {
        //        FactionName = "Sith",
        //        ToonsInGroup= GetMandatorySith(),

        //    })

        //    return toonsBreakDown;
        //}


        public static List<Yaz> GetYazometerToons(Player player)
        {
            List<Yaz> yazClass = new List<Yaz>();

            var myCharsMandatory = GetYazGearMandatory(player);
            foreach (var p in myCharsMandatory)
            {
                double score = 0;
                if (p.Gear == 11)
                {
                    score = p.Stars;
                }
                else if (p.Gear == 12)
                {
                    var g12 = p.Equipped.Count(pq => pq.Slot == 0 || pq.Slot == 1 || pq.Slot == 2);
                    var g12Plus = p.Equipped.Count(pq => pq.Slot == 3 || pq.Slot == 4);
                    score = 8 + (g12 == 3 ? 1 : 0) + (g12Plus == 2 ? 1 : 0);
                }

                yazClass.Add(new Yaz() { Score = score, ToonName = p.Name, FactionName = GetMandatoryToons().FirstOrDefault(v => v.Key == p.Name).Value, GroupName = "Mandatory", MaxScore = 10 });
            }

            var myCharsLegacy = GetYazGearLegacy(player);
            foreach (var p in myCharsLegacy)
            {
                double score = 0;
                if (p.Gear == 11)
                {
                    score = 1;
                }
                else if (p.Gear == 12)
                {
                    score = 3;
                }                

                yazClass.Add(new Yaz() { Score = score, ToonName = p.Name , FactionName = GetLegacyToons().FirstOrDefault(v => v.Key == p.Name).Value , GroupName ="Legacy", MaxScore=3 });
            }

            var myCharsBonus = GetYazGearBonus(player);
            foreach (var p in myCharsBonus)
            {
                double score = 0;
                if (p.Gear == 11)
                {
                    score = 2;
                }
                else if (p.Gear == 12)
                {
                    score = 4;
                }
                else if(p.Gear==10)
                {
                    score = 1;
                }

                yazClass.Add(new Yaz() { Score = score, ToonName = p.Name, FactionName = GetBonusToons().FirstOrDefault(v => v.Key == p.Name).Value, GroupName = "Bonus", MaxScore = 4 });
            }

            return yazClass;
        }

        public static List<Yaz> GetYazometerZeta(Player player)
        {
            List<Yaz> yazClass = new List<Yaz>();

            yazClass = GetCharsZetaCount(player);

            foreach (var unitName in GetYazZetaExists())
            {
                var unit = player.Characters.FirstOrDefault(p => p.Name == unitName);
                yazClass.Add(new Yaz() { ToonName = unit == null ? unitName : unit.Name, Score = unit == null ? 0 : (unit.Skills.Any(p => p.HasZeta) ? 1 : 0) * 10 });
            }

            return yazClass;
        }
    }
}
