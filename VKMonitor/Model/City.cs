namespace VKMonitor.Model
{
    public class City
    {
        public long? Id { get; set; }

        public string Title { get; set; }

        public string Region { get; set; }

        public string Area { get; set; }

        public bool Important { get; set; }

        public City(VkNet.Model.City city)
        {
            Id = city.Id;
            Title = city.Title;
            Region = city.Region;
            Area = city.Area;
            Important = city.Important;
        }
    }
}
