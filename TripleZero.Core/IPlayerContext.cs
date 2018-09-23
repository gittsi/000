using SWGoH.Model;
using System;

namespace TripleZero.Core
{
    public interface IPlayerContext
    {
        Player GetPlayerData(int allyCode);
        Player GetPlayerData(string alias);
    }
}
