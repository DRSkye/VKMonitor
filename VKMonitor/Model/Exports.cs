namespace VKMonitor.Model
{
    public class Exports
    {
        public bool Instagram { get; set; }

        public bool Facebook { get; set; }

        public bool Twitter { get; set; }

        public bool Livejournal { get; set; }

        public Exports(VkNet.Model.Exports exports)
        {
            Instagram = exports.Instagram;
            Facebook = exports.Facebook;
            Twitter = exports.Twitter;
            Livejournal = exports.Livejournal;
        }
    }
}
