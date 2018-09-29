using SWGoH.Model;
using System;
using System.Collections.Generic;
using System.Text;
using TripleZero.Repository.SWGoHHelp.Dto;

namespace TripleZero.Repository.SWGoHHelp
{
    public interface ISWGoHHelpGuildRepository
    {
        Guild GetGuild(int allyCode);
    }
}
