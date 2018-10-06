using SWGoH.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TripleZero.Bot.Helper
{
    public class YazClass
    {
        public string Name { get; set; }
        public int Score { get; set; }
    }

    

    public static class YazHelper
    {
        private static IEnumerable<Character> GetCharsGear(Player player)
        {
            

            return player.Characters.Where(p =>
            p.Name == "Darth Traya"
            || p.Name == "Darth Nihilus"
            || p.Name == "Darth Sion"
            || p.Name == "Count Dooku"
            || p.Name == "Sith Trooper"
            || p.Name == "Rey (Jedi Training)"
            || p.Name == "R2-D2"
            || p.Name == "BB-8"
            || p.Name == "Amilyn Holdo"
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
            || p.Name == "Visas Marr"
            || p.Name == "Emperor Palpatine"
            || p.Name == "Darth Vader"
            || p.Name == "Grand Moff Tarkin"
            || p.Name == "Shoretrooper"
            || p.Name == "General Kenobi"
            || p.Name == "Barriss Offee"
            || p.Name == "Wampa"
            || p.Name == "Bastila Shan"
            || p.Name == "Grand Master Yoda"
            || p.Name == "Hermit Yoda"
            || p.Name == "Ezra Bridger"
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
            || p.Name == "Grand Admiral Thrawn"
            || p.Name == "Magmatrooper"
            || p.Name == "Death Trooper"
            || p.Name == "Imperial Probe Droid"
            || p.Name == "Qi'ra"
            || p.Name == "Vandor Chewbacca"
            || p.Name == "L3-37"
            || p.Name == "Zaalbar"
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

        private static List<YazClass> GetCharsZetaCount(Player player)
        {
            List<YazClass> yazClass = new List<YazClass>();

            var unitName = "Darth Traya";
            var unit = player.Characters.FirstOrDefault(p => p.Name == unitName);
            yazClass.Add(new YazClass() { Name = unit == null ? unitName : unit.Name, Score = unit == null ? 0 : unit.Skills.Count(p => p.HasZeta) * 10 });

            unitName = "Kylo Ren (Unmasked)";
            unit = player.Characters.FirstOrDefault(p => p.Name == unitName);
            yazClass.Add(new YazClass() { Name = unit == null ? unitName : unit.Name, Score = unit == null ? 0 : unit.Skills.Count(p => p.HasZeta) * 10 });

            unitName = "Chewbacca";
            unit = player.Characters.FirstOrDefault(p => p.Name == unitName);
            yazClass.Add(new YazClass() { Name = unit == null ? unitName : unit.Name, Score = unit == null ? 0 : unit.Skills.Count(p => p.HasZeta) * 10 });

            unitName = "Asajj Ventress";
            unit = player.Characters.FirstOrDefault(p => p.Name == unitName);
            yazClass.Add(new YazClass() { Name = unit == null ? unitName : unit.Name, Score = unit == null ? 0 : unit.Skills.Count(p => p.HasZeta) * 10 });

            unitName = "Mother Talzin";
            unit = player.Characters.FirstOrDefault(p => p.Name == unitName);
            yazClass.Add(new YazClass() { Name = unit == null ? unitName : unit.Name, Score = unit == null ? 0 : unit.Skills.Count(p => p.HasZeta) * 10 });

            unitName = "R2-D2";
            unit = player.Characters.FirstOrDefault(p => p.Name == unitName);
            yazClass.Add(new YazClass() { Name = unit == null ? unitName : unit.Name, Score = unit == null ? 0 : unit.Skills.Count(p => p.HasZeta) * 10 });

            return yazClass;
        }

        public static List<YazClass> GetYazometerToons(Player player)
        {
            List<YazClass> yazClass = new List<YazClass>();

            var myChars = GetCharsGear(player);

            foreach (var p in myChars)
            {
                int score = 0;
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

                yazClass.Add(new YazClass() { Score = score, Name = p.Name });
            }

            return yazClass;
        }

        public static List<YazClass> GetYazometerZeta(Player player)
        {
            List<YazClass> yazClass = new List<YazClass>();

            yazClass = GetCharsZetaCount(player);

            var unitName = "Rey (Jedi Training)";
            var unit = player.Characters.FirstOrDefault(p => p.Name == unitName);
            yazClass.Add(new YazClass() { Name = unit == null ? unitName : unit.Name, Score = unit == null ? 0 : (unit.Skills.Any(p => p.HasZeta) ? 1 : 0) * 10 });

            unitName = "Bastila Shan";
            unit = player.Characters.FirstOrDefault(p => p.Name == unitName);
            yazClass.Add(new YazClass()  { Name = unit == null ? unitName : unit.Name, Score = unit == null ? 0 : (unit.Skills.Any(p => p.HasZeta) ? 1 : 0) * 10 });

            unitName = "Emperor Palpatine";
            unit = player.Characters.FirstOrDefault(p => p.Name == unitName);
            yazClass.Add(new YazClass()  { Name = unit == null ? unitName : unit.Name, Score = unit == null ? 0 : (unit.Skills.Any(p => p.HasZeta) ? 1 : 0) * 10 });

            unitName = "Han Solo";
            unit = player.Characters.FirstOrDefault(p => p.Name == unitName);
            yazClass.Add(new YazClass()  { Name = unit == null ? unitName : unit.Name, Score = unit == null ? 0 : (unit.Skills.Any(p => p.HasZeta) ? 1 : 0) * 10 });

            unitName = "Bossk";
            unit = player.Characters.FirstOrDefault(p => p.Name == unitName);
            yazClass.Add(new YazClass()  { Name = unit == null ? unitName : unit.Name, Score = unit == null ? 0 : (unit.Skills.Any(p => p.HasZeta) ? 1 : 0) * 10 });

            unitName = "BB-8";
            unit = player.Characters.FirstOrDefault(p => p.Name == unitName);
            yazClass.Add(new YazClass()  { Name = unit == null ? unitName : unit.Name, Score = unit == null ? 0 : (unit.Skills.Any(p => p.HasZeta) ? 1 : 0) * 10 });

            unitName = "General Veers";
            unit = player.Characters.FirstOrDefault(p => p.Name == unitName);
            yazClass.Add(new YazClass()  { Name = unit == null ? unitName : unit.Name, Score = unit == null ? 0 : (unit.Skills.Any(p => p.HasZeta) ? 1 : 0) * 10 });

            unitName = "Darth Sion";
            unit = player.Characters.FirstOrDefault(p => p.Name == unitName);
            yazClass.Add(new YazClass()  { Name = unit == null ? unitName : unit.Name, Score = unit == null ? 0 : (unit.Skills.Any(p => p.HasZeta) ? 1 : 0) * 10 });

            unitName = "Commander Luke Skywalker";
            unit = player.Characters.FirstOrDefault(p => p.Name == unitName);
            yazClass.Add(new YazClass()  { Name = unit == null ? unitName : unit.Name, Score = unit == null ? 0 : (unit.Skills.Any(p => p.HasZeta) ? 1 : 0) * 10 });

            unitName = "Grand Master Yoda";
            unit = player.Characters.FirstOrDefault(p => p.Name == unitName);
            yazClass.Add(new YazClass()  { Name = unit == null ? unitName : unit.Name, Score = unit == null ? 0 : (unit.Skills.Any(p => p.HasZeta) ? 1 : 0) * 10 });

            unitName = "Vandor Chewbacca";
            unit = player.Characters.FirstOrDefault(p => p.Name == unitName);
            yazClass.Add(new YazClass()  { Name = unit == null ? unitName : unit.Name, Score = unit == null ? 0 : (unit.Skills.Any(p => p.HasZeta) ? 1 : 0) * 10 });

            unitName = "Enfys Nest";
            unit = player.Characters.FirstOrDefault(p => p.Name == unitName);
            yazClass.Add(new YazClass()  { Name = unit == null ? unitName : unit.Name, Score = unit == null ? 0 : (unit.Skills.Any(p => p.HasZeta) ? 1 : 0) * 10 });

            unitName = "Kanan Jarrus";
            unit = player.Characters.FirstOrDefault(p => p.Name == unitName);
            yazClass.Add(new YazClass()  { Name = unit == null ? unitName : unit.Name, Score = unit == null ? 0 : (unit.Skills.Any(p => p.HasZeta) ? 1 : 0) * 10 });

            unitName = "Barriss Offee";
            unit = player.Characters.FirstOrDefault(p => p.Name == unitName);
            yazClass.Add(new YazClass()  { Name = unit == null ? unitName : unit.Name, Score = unit == null ? 0 : (unit.Skills.Any(p => p.HasZeta) ? 1 : 0) * 10 });

            unitName = "Visas Marr";
            unit = player.Characters.FirstOrDefault(p => p.Name == unitName);
            yazClass.Add(new YazClass()  { Name = unit == null ? unitName : unit.Name, Score = unit == null ? 0 : (unit.Skills.Any(p => p.HasZeta) ? 1 : 0) * 10 });

            unitName = "Finn";
            unit = player.Characters.FirstOrDefault(p => p.Name == unitName);
            yazClass.Add(new YazClass()  { Name = unit == null ? unitName : unit.Name, Score = unit == null ? 0 : (unit.Skills.Any(p => p.HasZeta) ? 1 : 0) * 10 });

            unitName = "Sabine Wren";
            unit = player.Characters.FirstOrDefault(p => p.Name == unitName);
            yazClass.Add(new YazClass()  { Name = unit == null ? unitName : unit.Name, Score = unit == null ? 0 : (unit.Skills.Any(p => p.HasZeta) ? 1 : 0) * 10 });

            unitName = "Hermit Yoda";
            unit = player.Characters.FirstOrDefault(p => p.Name == unitName);
            yazClass.Add(new YazClass()  { Name = unit == null ? unitName : unit.Name, Score = unit == null ? 0 : (unit.Skills.Any(p => p.HasZeta) ? 1 : 0) * 10 });

            unitName = "Chief Chirpa";
            unit = player.Characters.FirstOrDefault(p => p.Name == unitName);
            yazClass.Add(new YazClass()  { Name = unit == null ? unitName : unit.Name, Score = unit == null ? 0 : (unit.Skills.Any(p => p.HasZeta) ? 1 : 0) * 10 });

            unitName = "Wicket";
            unit = player.Characters.FirstOrDefault(p => p.Name == unitName);
            yazClass.Add(new YazClass()  { Name = unit == null ? unitName : unit.Name, Score = unit == null ? 0 : (unit.Skills.Any(p => p.HasZeta) ? 1 : 0) * 10 });           

            return yazClass;
        }
    }
}
