using System;
using System.Collections.Generic;
using System.Text;

namespace SWGoH.Model.Enums
{
    public enum QueueStatus
    {
        PendingProcess = 0,
        Processing = 1,
        Failed = 101
    }
    public enum QueueType
    {
        Player = 1,
        Guild = 2
    }

    public enum QueuePriority
    {
        AutoUpdate = 1,
        ManualLoad = 2
    }
}
