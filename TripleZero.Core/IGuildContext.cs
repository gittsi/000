using SWGoH.Model;
using System;
using System.Threading.Tasks;

namespace TripleZero.Core
{
    public interface IGuildContext
    {
        Task<Guild> GetGuildData(int allyCode);
        Task<Guild> GetGuildData(string alias);
    }
}
