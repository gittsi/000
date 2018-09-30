using SWGoH.Model;
using System.Threading.Tasks;

namespace TripleZero.Repository.SWGoHHelp
{
    public interface ISWGoHHelpGuildRepository
    {
        Task<Guild> GetGuild(int allyCode);
    }
}
