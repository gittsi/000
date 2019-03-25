using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using TripleZero.Repository.EnumDto;

namespace TripleZero.Repository.Dto
{   
    public class QueueDto
    {
        //public ObjectId? Id { get; set; }
        public string Id { get; set; }
        [BsonElement("itemId")]
        public string ItemId { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("insertedDate")]
        public string InsertedDate { get; set; }
        [BsonElement("processingStartDate")]
        public string ProcessingStartDate { get; set; }
        [BsonElement("nextRunDate")]
        public string NextRunDate { get; set; }
        [BsonElement("priority")]
        public QueuePriority Priority { get; set; }
        [BsonElement("status")]
        public QueueStatus Status { get; set; }
        [BsonElement("type")]
        public QueueType Type { get; set; }
        [BsonElement("processingBy")]
        public string ProcessingBy { get; set; }
    }
}
