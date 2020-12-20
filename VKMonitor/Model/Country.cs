namespace VKMonitor.Model
{
    public class Country
    {
        public long? Id { get; set; }

        public string Title { get; set; }

        public Country(VkNet.Model.Country country)
        {
            Id = country.Id;
            Title = country.Title;
        }
    }
}
