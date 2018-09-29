using SWGoH.Model;
using System;

namespace TripleZero.Core
{
    public interface IGuildContext
    {
        Guild GetGuildData(int allyCode);
        Guild GetGuildData(string alias);
    }
}
