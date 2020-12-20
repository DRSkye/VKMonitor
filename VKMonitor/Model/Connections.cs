namespace VKMonitor.Model
{
    public class Connections
    {
        public string FacebookId { get; set; }

        public string FacebookName { get; set; }

        public string Instagram { get; set; }

        public string Skype { get; set; }

        public string Twitter { get; set; }

        public Connections(VkNet.Model.Connections connections)
        {
            if (connections == null)
                return;

            FacebookId = connections.Facebook;
            FacebookName = connections.FacebookName;
            Instagram = connections.Instagram;
            Skype = connections.Skype;
            Twitter = connections.Twitter;
        }
    }
}
