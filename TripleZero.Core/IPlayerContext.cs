using SWGoH.Model;
using System;
using System.Threading.Tasks;

namespace TripleZero.Core
{
    public interface IPlayerContext
    {
        Task<Player> GetPlayerData(int allyCode);
        Task<Player> GetPlayerData(string alias);
    }
}
