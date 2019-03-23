using SWGoH.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SWGoH.Model
{
    public class Queue
    {
        public string Id { get; set; }
        public string ObjectId { get; set; }
        public string Name { get; set; }       
        public DateTime InsertedDate { get; set; }        
        public DateTime? NextRunDate { get; set; }
        public QueuePriority Priority { get; set; }
        public QueueStatus Status { get; set; }
        public QueueType Type { get; set; }
        public string ProcessingBy { get; set; }
        public DateTime? ProcessingStartDate { get; set; }
    }
}
