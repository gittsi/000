using SWGoH.Model;
using System.Threading.Tasks;

namespace TripleZero.Repository.SWGoHHelpRepository
{
    public interface ISWGoHHelpPlayerRepository
    {
        Task<Player> GetPlayer(int allyCode);
    }
}
