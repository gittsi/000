using SWGoH.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TripleZero.Repository.MongoDBRepository
{
    public interface IGuildConfigRepository
    {
        string CollectionName { get; }
        Task<List<GuildConfig>> GetAll();
    }
}
