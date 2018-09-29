using SWGoH.Model;
using System;
using System.Collections.Generic;
using System.Text;
using TripleZero.Repository.SWGoHHelp.Dto;

namespace TripleZero.Repository.SWGoHHelpRepository
{
    public interface ISWGoHHelpPlayerRepository
    {
        Player GetPlayer(int allyCode);
    }
}
