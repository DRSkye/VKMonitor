namespace VKMonitor.Model
{
    public class Contacts
    {
        public string MobilePhone { get; set; }
        
        public string HomePhone { get; set; }

        public Contacts(VkNet.Model.Contacts contacts)
        {
            if (contacts == null)
                return;

            MobilePhone = contacts.MobilePhone;
            HomePhone = contacts.HomePhone;
        }
    }
}
