using Discord.Commands;
using System.Threading.Tasks;
using TeamComparisonGA;
namespace TripleZero.Bot.Modules
{
    [Name("Fun")]
    [Summary("Have some fun with commands!!!")]
    public class GrandArenaTeams : ModuleBase<SocketCommandContext>
    {
        [Command("ga", RunMode = RunMode.Async)]
        [Summary("Compare most used Teams for Grand Arena")]
        [Remarks("*ga <allycode1> <allycode1>*")]
        public async Task Say(string friendlyID, string foeID)
        {
            GetAllData getAllData = new GetAllData();
            var reply = await getAllData.Fetcher(friendlyID, foeID);

            await ReplyAsync(reply);
        }
    }
}