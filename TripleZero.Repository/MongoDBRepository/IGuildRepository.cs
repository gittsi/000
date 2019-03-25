using SWGoH.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TripleZero.Repository.MongoDBRepository
{
    public interface IGuildRepository
    {
        string CollectionName { get; }
        Task<List<Guild>> GetAll();
        Task<Guild> GetGuildByAlias(string alias);
        Task<Guild> GetGuildByName(string name);
    }
}
