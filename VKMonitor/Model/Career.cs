namespace VKMonitor.Model
{
    public class Career
    {
        public long? GroupId { get; set; }

        public string Company { get; set; }

        public long? CountryId { get; set; }

        public long? CityId { get; set; }

        public string CityName { get; set; }

        public int? From { get; set; }

        public ulong? Until { get; set; }

        public string Position { get; set; }

        public Career(VkNet.Model.Career career)
        {
            GroupId = career.GroupId;
            Company = career.Company;
            CountryId = career.CountryId;
            CityId = career.CityId;
            CityName = career.CityName;
            From = career.From;
            Until = career.Until;
            Position = career.Position;
        }
    }
}
