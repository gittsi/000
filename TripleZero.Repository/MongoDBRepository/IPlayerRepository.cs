using SWGoH.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TripleZero.Repository.MongoDBRepository
{
    public interface IPlayerRepository
    {
        string CollectionName { get; }
        Task<List<Player>> GetAll();
        Task<Player> GetByName(string name);
        Task<Player> GetByAllyCode(string allyCode);
    }
}
