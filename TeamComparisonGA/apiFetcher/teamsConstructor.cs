using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace TeamComparisonGA.getTeamsData
{
    class TeamsConstructor
    {
        public async  Task<string> GetTeamData(JObject[] rawData)
        {
            var predefinedTeams = rawData[2]["teams"];
            var friend = rawData[0];
            var foe = rawData[1];
            var finalText = "";
            var teamText = "";
            for (int i=0; i<predefinedTeams.Count(); i++)
            {
                 teamText = await ConstructTeamText(predefinedTeams[i], friend, foe);
                 finalText = finalText + teamText;
            }
            
            return finalText;
        }

        
        static async Task<string> ConstructTeamText (JToken predefinedTeams ,JToken friend, JToken foe)
        {
            string finalText = "";
            string textFriend = "";
            string textFoe = "";
            var toons = predefinedTeams["toons"];
            string teamName = $"Team {predefinedTeams["teamName"]}";
            finalText = $"{ teamName}\n";
            for (int i=0; i<toons.Count(); i++)
            {
                textFriend = textFriend + GetToonData(toons[i], friend["units"]);
                textFoe =  textFoe + GetToonData(toons[i], foe["units"]);             
            }
            await GetNames(friend["units"]);
            finalText = $"{finalText}{friend["data"]["name"]}\n{textFriend}\n{foe["data"]["name"]}\n{textFoe}\n";
            return finalText;
        }

        static string GetToonData (JToken toon, JToken data)
        {
            for (int i=0; i<data.Count(); i++)
            { 
                if (data[i]["data"]["name"].ToString() == toon.ToString())
                {
                  return $"{toon.ToString()}: {data[i]["data"]["rarity"]}*, G{data[i]["data"]["gear_level"]}, Speed: {data[i]["data"]["stats"]["5"]}, Zeta:{data[i]["data"]["zeta_abilities"].Count()} \n";
                }
            }
            return null;
        }

        static async Task GetNames(JToken data)
        {
            await Task.Run(() =>
            {
                List<string> toonNames = new List<string>();
                string dir = Path.Combine(Directory.GetCurrentDirectory(), "configuration").ToString();
                string file = Path.Combine(dir, "toonNames.txt").ToString();
                for (int i = 0; i < data.Count(); i++)
                {
                    toonNames.Add(data[i]["data"]["name"].ToString());
                }
                toonNames = toonNames.OrderBy(i => i).ToList();
                TextWriter tw = new StreamWriter(file);
                foreach (String s in toonNames)
                    tw.WriteLine(s);
                tw.Close();
            });

        }
    }
}
