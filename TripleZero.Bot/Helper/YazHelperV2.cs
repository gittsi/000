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
        public static double TotalPointsZeta = 300;

        //mandatory
        public static double TotalPointsMandaToryToons => GetMandatoryToons().Count * 10;

        public static double GetSithTotalPoints => GetMandatorySith().Count * 10;
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
        private static List<string> GetMandatorySith()
        {
            var list = new List<string>();
            list.Add("Darth Traya");
            list.Add("Darth Nihilus");
            list.Add("Darth Sion");
            list.Add("Bastila Shan (Fallen)");
            list.Add("Sith Trooper");
            //list.Add("Count Dooku");
            return list;
        }

        private static List<string> GetMandatoryJedi()
        {
            var list = new List<string>();
            list.Add("Jedi Knight Revan");
            list.Add("Jolee Bindo");
            list.Add("Grand Master Yoda");
            list.Add("General Kenobi");
            list.Add("Hermit Yoda");
            list.Add("Bastila Shan");
            list.Add("Barriss Offee");
            return list;
        }

        private static List<string> GetMandatoryResistance()
        {
            var list = new List<string>();
            list.Add("Rey (Jedi Training)");
            list.Add("R2-D2");
            list.Add("BB-8");
            //list.Add("Amilyn Holdo");
            return list;
        }

        private static List<string> GetMandatoryRebels()
        {
            var list = new List<string>();
            list.Add("Commander Luke Skywalker");
            list.Add("Han Solo");
            list.Add("Chewbacca");
            list.Add("Obi-Wan Kenobi (Old Ben)");
            list.Add("C-3PO");
            return list;
        }

        private static List<string> GetMandatoryBountyHunters()
        {
            var list = new List<string>();
            list.Add("Bossk");
            list.Add("Jango Fett");
            list.Add("Dengar");
            list.Add("Boba Fett");
            return list;
        }

        private static List<string> GetMandatoryEmpire()
        {
            var list = new List<string>();
            list.Add("Emperor Palpatine");
            list.Add("Darth Vader");
            list.Add("Grand Admiral Thrawn");
            list.Add("Shoretrooper");
            list.Add("Death Trooper");
            return list;
        }

        private static List<string> GetMandatoryNightSisters()
        {
            var list = new List<string>();
            list.Add("Asajj Ventress");
            list.Add("Mother Talzin");
            list.Add("Old Daka");
            list.Add("Nightsister Spirit");
            list.Add("Nightsister Zombie");
            return list;
        }

        private static List<string> GetMandatoryScoundrels()
        {
            var list = new List<string>();
            list.Add("Qi'ra");
            list.Add("Vandor Chewbacca");
            list.Add("L3-37");
            list.Add("Enfys Nest");
            return list;
        }

        private static List<string> GetMandatoryOldRepublic()
        {
            var list = new List<string>();
            list.Add("Zaalbar");
            list.Add("Mission Vao");
            list.Add("Carth Onasi");
            list.Add("Canderous Ordo");
            return list;
        }

        private static List<string> GetMandatoryOthers()
        {
            var list = new List<string>();
            list.Add("Wampa");
            list.Add("General Grievous");
            list.Add("B2 Super Battle Droid");
            list.Add("CT-7567 \"Rex\"");
            list.Add("Chirrut Îmwe");
            list.Add("Baze Malbus");
            return list;
        }

        private static List<string> GetMandatoryToons()
        {
            var list = new List<string>();
            //Sith
            list.AddRange(GetMandatorySith());

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
        private static List<string> GetLegacyFirstOrder()
        {
            var list = new List<string>();
            list.Add("Kylo Ren (Unmasked)");
            list.Add("Kylo Ren");
            list.Add("First Order Stormtrooper");
            list.Add("First Order Officer");
            list.Add("First Order Executioner");
            return list;
        }

        private static List<string> GetLegacyImperials()
        {
            var list = new List<string>();
            list.Add("General Veers");
            list.Add("Colonel Starck");
            list.Add("Snowtrooper");
            //list.Add("Stormtrooper");
            list.Add("Range Trooper");
            list.Add("Magmatrooper");
            return list;
        }

        private static List<string> GetLegacyPhoenix()
        {
            var list = new List<string>();
            list.Add("Ezra Bridger");
            list.Add("Hera Syndulla");
            list.Add("Kanan Jarrus");
            list.Add("Sabine Wren");
            list.Add("Garazeb \"Zeb\" Orrelios");
            return list;
        }

        private static List<string> GetLegacyResistance()
        {
            var list = new List<string>();
            list.Add("Finn");
            list.Add("Poe Dameron");
            list.Add("Rey (Scavenger)");
            list.Add("Resistance Pilot");
            list.Add("Resistance Trooper");
            return list;
        }

        private static List<string> GetLegacyToons()
        {
            var list = new List<string>();

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
        private static List<string> GetBonusClones()
        {
            var list = new List<string>();
            list.Add("CC-2224 \"Cody\"");
            list.Add("Clone Sergeant - Phase I");
            list.Add("CT-21-0408 \"Echo\"");
            list.Add("CT-5555 \"Fives\"");
            return list;
        }
        private static List<string> GetBonusEwoks()
        {
            var list = new List<string>();
            list.Add("Chief Chirpa");
            list.Add("Wicket");
            list.Add("Ewok Elder");
            list.Add("Paploo");
            list.Add("Logray");

            return list;
        }
        private static List<string> GetBonusJawa()
        {
            var list = new List<string>();
            list.Add("Chief Nebit");
            list.Add("Jawa");
            list.Add("Jawa Engineer");
            list.Add("Java Scavenger");
            list.Add("Dathcha");

            return list;
        }
        private static List<string> GetBonusOthers()
        {
            var list = new List<string>();
            list.Add("Mace Windu");
            list.Add("CountDooku");

            return list;
        }

        private static List<string> GetBonusToons()
        {
            var list = new List<string>();

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
            return player.Characters.Where(p => GetMandatoryToons().Contains(p.Name));
        }

        private static IEnumerable<Character> GetYazGearLegacy(Player player)
        {
            return player.Characters.Where(p => GetLegacyToons().Contains(p.Name));
        }

        private static IEnumerable<Character> GetYazGearBonus(Player player)
        {
            return player.Characters.Where(p => GetBonusToons().Contains(p.Name));
        }



        private static List<Yaz> GetCharsZetaCount(Player player)
        {
            List<Yaz> yazClass = new List<Yaz>();

            foreach (var unitName in GetYazZetaCount())
            {
                var unit = player.Characters.FirstOrDefault(p => p.Name == unitName);
                yazClass.Add(new Yaz() { Name = unit == null ? unitName : unit.Name, Score = unit == null ? 0 : unit.Skills.Count(p => p.HasZeta) * 10 });
            }
            return yazClass;
        }

        public static List<GroupYaz> GetToonsBreakDown(Player player)
        {
            List<GroupYaz> toonsBreakDown = new List<GroupYaz>();

            GroupYaz mandatoryToons = new GroupYaz()
            {
                GroupName = "Mandatory Toons"                
            };
            GroupYaz legacyToons = new GroupYaz()
            {
                GroupName = "Legacy Toons"
            };
            GroupYaz bonusToons = new GroupYaz()
            {
                GroupName = "Bonus Toons"
            };

            return toonsBreakDown;
        }


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

                yazClass.Add(new Yaz() { Score = score, Name = p.Name });
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

                yazClass.Add(new Yaz() { Score = score, Name = p.Name });
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

                yazClass.Add(new Yaz() { Score = score, Name = p.Name });
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
                yazClass.Add(new Yaz() { Name = unit == null ? unitName : unit.Name, Score = unit == null ? 0 : (unit.Skills.Any(p => p.HasZeta) ? 1 : 0) * 10 });
            }

            return yazClass;
        }
    }
}
