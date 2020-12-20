using System;

namespace VKMonitor.Model
{
    public class LastSeen
    {
        public string Platform { get; set; }

        public DateTime? Time { get; set; }

        public LastSeen(VkNet.Model.LastSeen lastSeen)
        {
            if (lastSeen == null)
                return;

            Platform = lastSeen.Platform;
            Time = lastSeen.Time;
        }
    }
}
