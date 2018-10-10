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
        public static double GetTotalPoints => 1080.0  + 72.5;

        private static IEnumerable<Character> GetYazGearPremium(Player player)
        {
            return player.Characters.Where(p =>
            p.Name == "Darth Traya"
            || p.Name == "Darth Nihilus"
            || p.Name == "Darth Sion"
            || p.Name == "Count Dooku"
            || p.Name == "Sith Trooper"
            || p.Name == "Rey (Jedi Training)"
            || p.Name == "Bossk"
            || p.Name == "Boba Fett"
            || p.Name == "Jango Fett"
            || p.Name == "Dengar"
            || p.Name == "Kylo Ren (Unmasked)"
            || p.Name == "Kylo Ren"
            || p.Name == "First Order Stormtrooper"
            || p.Name == "First Order Officer"
            || p.Name == "First Order Executioner"
            || p.Name == "Commander Luke Skywalker"
            || p.Name == "Enfys Nest"
            || p.Name == "Han Solo"
            || p.Name == "Chewbacca"
            || p.Name == "Wampa"
            || p.Name == "Bastila Shan"
            || p.Name == "Grand Master Yoda"
            || p.Name == "Hermit Yoda"
            || p.Name == "Ezra Bridger"
            || p.Name == "Grand Admiral Thrawn"
            || p.Name == "Magmatrooper"
            || p.Name == "Qi'ra"
            || p.Name == "Vandor Chewbacca"
            || p.Name == "L3-37"
            || p.Name == "Zaalbar"


           
            );
        }

        private static IEnumerable<Character> GetYazGearNormal(Player player)
        {
            return player.Characters.Where(p =>
             p.Name == "R2-D2"
            || p.Name == "BB-8"
            || p.Name == "Amilyn Holdo"
            || p.Name == "Visas Marr"
            || p.Name == "Emperor Palpatine"
            || p.Name == "Darth Vader"
            || p.Name == "Grand Moff Tarkin"
            || p.Name == "Shoretrooper"
            || p.Name == "General Kenobi"
            || p.Name == "Barriss Offee"
            || p.Name == "Asajj Ventress"
            || p.Name == "Mother Talzin"
            || p.Name == "Old Daka"
            || p.Name == "Talia"
            || p.Name == "Nightsister Zombie"
            || p.Name == "General Veers"
            || p.Name == "Colonel Starck"
            || p.Name == "Snowtrooper"
            || p.Name == "Stormtrooper"
            || p.Name == "Range Trooper"
            || p.Name == "Death Trooper"
            || p.Name == "Imperial Probe Droid"
            || p.Name == "Hera Syndulla"
            || p.Name == "Kanan Jarrus"
            || p.Name == "Sabine Wren"
            || p.Name == "Chopper"
            || p.Name == "Garazeb \"Zeb\" Orrelios"
            || p.Name == "Chief Chirpa"
            || p.Name == "Wicket"
            || p.Name == "Ewok Elder"
            || p.Name == "Paploo"
            || p.Name == "Logray"
            || p.Name == "Finn"
            || p.Name == "Poe Dameron"
            || p.Name == "Rey (Scavenger)"
            || p.Name == "Resistance Pilot"
            || p.Name == "Resistance Trooper"
            );
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

            return yazList;
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



        public static List<Yaz> GetYazometerShips(Player player)
        {
            List<Yaz> yazClass = new List<Yaz>();
            
            foreach (var unitName in GetShips())
            {
                var ship = player.Ships.FirstOrDefault(p => p.Name == unitName);
                var unit = player.Characters.FirstOrDefault(p => p.Id == ship.Crew.FirstOrDefault().Id);
                //yazClass.Add(new Yaz() { Name = ship == null ? unitName : ship.Name, Score = ship == null ? 0 : (ship.Skills.Any(p => p.HasZeta) ? 1 : 0) * 10 });
                var score = 0;
                if (ship?.Stars == 7) score += 2;
                if (unit?.Stars == 7) score += 2;
                if (!ship.Abilities.Any(p => p.Level != p.MaxLevel)) score += 2;
                if (unit.Gear == 12) score += 2;
                if(unit.Name == "Plo Koon" || unit.Name == "Sun Fac" )
                {
                    var countSkills = unit.Skills.Count;
                    var count8 = unit.Skills.Count(p => p.Tier == 8);
                    var count7 = unit.Skills.Count(p => p.Tier == 7);

                    if(countSkills == count8 + count7  && count7 == 1) score+=1;
                }
                else
                    if (!unit.Skills.Any(p => p.Tier < 8)  || unit.Skills.Any(p=>p.IsZeta)) score += 1;

                if (unit.Gear == 12 && unit.Equipped.Count >= 3) score += 1;


                yazClass.Add(new Yaz() { Name = unitName, Score = score });
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

                yazClass.Add(new Yaz() { Score = score* multiplier, Name = p.Name });
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

                yazClass.Add(new Yaz() { Score = score * multiplier, Name = p.Name });
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
                yazClass.Add(new Yaz() { Name = unit == null ? unitName : unit.Name, Score = unit == null ? 0 : (unit.Skills.Any(p => p.HasZeta) ? 1 : 0) * 10 });
            }

            return yazClass;
        }
    }
}
