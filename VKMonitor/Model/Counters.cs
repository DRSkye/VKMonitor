namespace VKMonitor.Model
{
    public class Counters
    {
        public int? Albums { get; set; }

        public int? Videos { get; set; }

        public int? Audios { get; set; }

        public int? Photos { get; set; }

        public int? Notes { get; set; }

        public int? Friends { get; set; }

        public int? Groups { get; set; }

        public int? OnlineFriends { get; set; }

        public int? MutualFriends { get; set; }

        public int? UserVideos { get; set; }

        public int? Followers { get; set; }

        public int? Pages { get; set; }

        public Counters(VkNet.Model.Counters counters)
        {
            if (counters == null)
                return;

            Albums = counters.Albums;
            Videos = counters.Videos;
            Audios = counters.Audios;
            Photos = counters.Photos;
            Notes = counters.Notes;
            Friends = counters.Friends;
            Groups = counters.Groups;
            OnlineFriends = counters.OnlineFriends;
            MutualFriends = counters.MutualFriends;
            UserVideos = counters.UserVideos;
            Followers = counters.Followers;
            Pages = counters.Pages;
        }
    }
}
