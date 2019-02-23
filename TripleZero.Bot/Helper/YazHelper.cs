using SWGoH.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TripleZero.Bot.Model;

namespace TripleZero.Bot.Helper
{
    [Obsolete("Older version!!!")]
    public static class YazHelperOLD
    {
        public static double GetTotalPoints => GetPremium().Count*12.5 + GetNormal().Count*10 + GetLow().Count*8 + GetShips().Count*10 + 350 ;
       
        private static List<string> GetPremium()
        {
            var list = new List<string>();
            list.Add("Darth Traya");
            list.Add("Darth Nihilus");
            list.Add("Darth Sion");
            list.Add("Count Dooku");
            list.Add("Sith Trooper");
            list.Add("Rey (Jedi Training)");
            list.Add("Bossk");
            list.Add("Boba Fett");
            list.Add("Jango Fett");
            list.Add("Dengar");
            list.Add("Kylo Ren (Unmasked)");
            list.Add("Kylo Ren");
            list.Add("First Order Stormtrooper");
            list.Add("First Order Officer");
            list.Add("First Order Executioner");
            list.Add("Commander Luke Skywalker");
            list.Add("Enfys Nest");
            list.Add("Han Solo");
            list.Add("Chewbacca");
            list.Add("Wampa");
            list.Add("Bastila Shan");
            list.Add("Grand Master Yoda");
            list.Add("Hermit Yoda");
            list.Add("Ezra Bridger");
            list.Add("Grand Admiral Thrawn");
            list.Add("Magmatrooper");
            list.Add("Qi'ra");
            list.Add("Vandor Chewbacca");
            list.Add("L3-37");
            list.Add("Zaalbar");
            list.Add("Jedi Knight Revan");
            list.Add("Jolee Bindo");

            return list;
        }

        private static List<string> GetNormal()
        {
            var list = new List<string>();
            list.Add("R2-D2");
            list.Add("BB-8");
            list.Add("Amilyn Holdo");
            list.Add("Visas Marr");
            list.Add("Emperor Palpatine");
            list.Add("Darth Vader");
            list.Add("Grand Moff Tarkin");
            list.Add("Shoretrooper");
            list.Add("General Kenobi");
            list.Add("Barriss Offee");
            list.Add("Asajj Ventress");
            list.Add("Mother Talzin");
            list.Add("Old Daka");
            list.Add("Talia");
            list.Add("Nightsister Zombie");
            list.Add("General Veers");
            list.Add("Colonel Starck");
            list.Add("Snowtrooper");
            list.Add("Stormtrooper");
            list.Add("Range Trooper");
            list.Add("Death Trooper");
            list.Add("Imperial Probe Droid");
            list.Add("Hera Syndulla");
            list.Add("Kanan Jarrus");
            list.Add("Sabine Wren");
            list.Add("Chopper");
            list.Add("Garazeb \"Zeb\" Orrelios");

            return list;
        }

        private static List<string> GetLow()
        {
            var list = new List<string>();
            list.Add("Chief Chirpa");
            list.Add("Wicket");
            list.Add("Ewok Elder");
            list.Add("Paploo");
            list.Add("Logray");
            list.Add("Finn");
            list.Add("Poe Dameron");
            list.Add("Rey (Scavenger)");
            list.Add("Resistance Pilot");
            list.Add("Resistance Trooper");

            return list;
        }

        private static List<string> GetYazZetaCount()
        {
            List<string> yazList = new List<string>();
            yazList.Add("Darth Traya");
            yazList.Add("Kylo Ren (Unmasked)");
            yazList.Add("Chewbacca");
            yazList.Add("Asajj Ventress");
            yazList.Add("Mother Talzin");
            yazList.Add("R2-D2");

            return yazList;

        }

        private static List<string> GetYazZetaExists()
        {
            List<string> yazList = new List<string>();
            yazList.Add("Rey (Jedi Training)");
            yazList.Add("Bastila Shan");
            yazList.Add("Emperor Palpatine");
            yazList.Add("Han Solo");
            yazList.Add("Bossk");
            yazList.Add("BB-8");
            yazList.Add("General Veers");
            yazList.Add("Darth Sion");
            yazList.Add("Commander Luke Skywalker");
            yazList.Add("Grand Master Yoda");
            yazList.Add("Vandor Chewbacca");
            yazList.Add("Enfys Nest");
            yazList.Add("Kanan Jarrus");
            yazList.Add("Barriss Offee");
            yazList.Add("Visas Marr");
            yazList.Add("Finn");
            yazList.Add("Sabine Wren");
            yazList.Add("Hermit Yoda");
            yazList.Add("Chief Chirpa");
            yazList.Add("Wicket");
            yazList.Add("Jedi Knight Revan");
            yazList.Add("Jolee Bindo");

            return yazList;
        }

        private static List<string> GetShips()
        {
            List<string> yazList = new List<string>();
            yazList.Add("Chimaera");
            yazList.Add("Executrix");
            yazList.Add("Geonosian Soldier's Starfighter");
            yazList.Add("Geonosian Spy's Starfighter");
            yazList.Add("Sun Fac's Geonosian Starfighter");
            yazList.Add("TIE Advanced x1");
            yazList.Add("Imperial TIE Fighter");
            yazList.Add("Biggs Darklighter's X-wing");
            yazList.Add("Hound's Tooth");
            yazList.Add("Plo Koon's Jedi Starfighter");
            yazList.Add("Xanadu Blood");

            return yazList;
        }

        private static IEnumerable<Character> GetYazGearPremium(Player player)
        {
            return player.Characters.Where(p =>GetPremium().Contains(p.Name));
        }

        private static IEnumerable<Character> GetYazGearNormal(Player player)
        {
            return player.Characters.Where(p => GetNormal().Contains(p.Name));            
        }

        private static IEnumerable<Character> GetYazGearLow(Player player)
        {
            return player.Characters.Where(p => GetLow().Contains(p.Name));
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



        public static List<Yaz> GetYazometerShips(Player player)
        {
            List<Yaz> yazClass = new List<Yaz>();
            
            foreach (var unitName in GetShips())
            {
                var ship = player.Ships.FirstOrDefault(p => p.Name == unitName);
                var unit = player.Characters.FirstOrDefault(p => p.Id == ship?.Crew?.FirstOrDefault().Id);
                //yazClass.Add(new Yaz() { Name = ship == null ? unitName : ship.Name, Score = ship == null ? 0 : (ship.Skills.Any(p => p.HasZeta) ? 1 : 0) * 10 });
                var score = 0;
                if (ship?.Stars == 7) score += 2;
                if (unit?.Stars == 7) score += 2;
                if (!ship?.Abilities?.Any(p => p?.Level != p?.MaxLevel) == true ) score += 2;
                if (unit?.Gear == 12) score += 2;
                if(unit?.Name == "Plo Koon" || unit?.Name == "Sun Fac" )
                {
                    var countSkills = unit.Skills.Count;
                    var count8 = unit.Skills.Count(p => p.Tier == 8);
                    var count7 = unit.Skills.Count(p => p.Tier == 7);

                    if(countSkills == count8 + count7  && count7 == 1) score+=1;
                }
                else
                    if (!unit?.Skills?.Any(p => p?.Tier < 8) == true  || unit?.Skills?.Any(p=>p.IsZeta) == true) score += 1;

                if (unit?.Gear == 12 && unit?.Equipped?.Count >= 3) score += 1;


                yazClass.Add(new Yaz() { ToonName = unitName, Score = score });
            }

            return yazClass;
        }


        public static List<Yaz> GetYazometerToons(Player player)
        {
            

            var myCharsNormal = GetYazGearNormal(player);
            double multiplier = 1;

            List<Yaz> yazClass = new List<Yaz>();
            foreach (var p in myCharsNormal)
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
                    score = 8 + (g12 >= 2 ? 1 : 0) + (g12Plus >= 1 ? 1 : 0);
                }

                yazClass.Add(new Yaz() { Score = score* multiplier, ToonName = p.Name });
            }

            var myCharsPremium = GetYazGearPremium(player);
            multiplier = 1.25;           
            foreach (var p in myCharsPremium)
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
                    score = 8 + (g12 >= 2 ? 1 : 0) + (g12Plus >= 1 ? 1 : 0);
                }

                yazClass.Add(new Yaz() { Score = score * multiplier, ToonName = p.Name });
            }

            var myCharsLow = GetYazGearLow(player);
            multiplier = 0.8;
            foreach (var p in myCharsLow)
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
                    score = 8 + (g12 >= 2 ? 1 : 0) + (g12Plus >= 1 ? 1 : 0);
                }

                yazClass.Add(new Yaz() { Score = score * multiplier, ToonName = p.Name });
            }

            return yazClass;
        }

        public static List<Yaz> GetYazometerZeta(Player player)
        {
            List<Yaz> yazClass = new List<Yaz>();

            yazClass = GetCharsZetaCount(player);

            foreach(var unitName in GetYazZetaExists())
            {
                var unit = player.Characters.FirstOrDefault(p => p.Name == unitName);
                yazClass.Add(new Yaz() { ToonName = unit == null ? unitName : unit.Name, Score = unit == null ? 0 : (unit.Skills.Any(p => p.HasZeta) ? 1 : 0) * 10 });
            }

            return yazClass;
        }
    }
}
