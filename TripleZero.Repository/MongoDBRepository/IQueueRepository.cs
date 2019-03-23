using SWGoH.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TripleZero.Repository.MongoDBRepository
{
    public interface IQueueRepository
    {
        string CollectionName { get; }
        Task<List<Queue>> GetAll();
        Task<Queue> GetNextInQueue();
        Task<bool> ChangeQueueStatus(Queue queue);
    }
}