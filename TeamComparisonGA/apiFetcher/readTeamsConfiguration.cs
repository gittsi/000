using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace TeamComparisonGA.readTeams
{
    class TeamsConfiguration
    {
        public async Task<JObject> ReadFile()
        {
            string dir = Path.Combine(Directory.GetCurrentDirectory(), "configuration").ToString();
            string file = Path.Combine(dir, "teams.json").ToString();

            try
            {
                string text = await System.IO.File.ReadAllTextAsync(file);
                JObject teams = JObject.Parse(text);
                return teams;
            }
            catch (Exception error)
            {
                Console.WriteLine(error);
                return null;
            }
        }
    }
}
