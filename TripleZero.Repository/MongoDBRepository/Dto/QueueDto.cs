using MongoDB.Bson;
using System;
using TripleZero.Repository.EnumDto;

namespace TripleZero.Repository.Dto
{   
    public class QueueDto
    {
        public ObjectId? Id { get; set; }
        public string ObjectId { get; set; }
        public string Name { get; set; }               
        public string InsertedDate { get; set; }        
        public string ProcessingStartDate { get; set; }        
        public string NextRunDate { get; set; }
        public QueuePriority Priority { get; set; }
        public QueueStatus Status { get; set; }
        public QueueType Type { get; set; }
        public string ProcessingBy { get; set; }
    }
}
