using SWGoH.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TripleZero.Core
{
    public interface ICharacterConfigContext
    {
        Task<List<CharacterConfig>> GetCharactersConfig();
    }
}
