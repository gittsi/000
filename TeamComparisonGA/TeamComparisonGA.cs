using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using TeamComparisonGA.getTeamsData;

namespace TeamComparisonGA
{
    public class GetAllData
    {

        public async Task<string> Fetcher(/*string[] args , */ string friendlyID, string foeID)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            //todo remove builtin string when convert it to lib
            //var friendlyID = "251734828";
            //var foeID = "462747278";
            Console.WriteLine("Version 1.0.0");
            Console.WriteLine($"Grand Arena teams results for player {friendlyID} and player {foeID}");
            Console.WriteLine("Fetching Results from API");
            ApiFetcher apiFetcher = new ApiFetcher();
            var result = await apiFetcher.GetResult(friendlyID, foeID);

            TeamsConstructor teamsConstructor = new TeamsConstructor();
            var finalText = await teamsConstructor.GetTeamData(result);
            //Convert Data to output string
            //Console.WriteLine(finalText);
            stopwatch.Stop();
            Console.WriteLine($"Time to complete: {stopwatch.Elapsed}");
            return finalText;


        }

    }
}
