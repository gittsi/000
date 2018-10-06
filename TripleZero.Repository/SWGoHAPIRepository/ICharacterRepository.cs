using SWGoH.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TripleZero.Repository.SWGoHAPIRepository
{
    public interface ICharacterRepository
    {
        string Url { get; }
        Task<List<CharacterConfig>> GetCharacters();        
    }
}
