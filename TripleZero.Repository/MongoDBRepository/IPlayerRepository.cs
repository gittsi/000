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
        Task<Player> Get(string searchString);
    }
}
