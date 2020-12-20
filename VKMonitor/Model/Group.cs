using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKMonitor.Model
{
    public class Group
    {
        public long Id { get; private set; }

        public string Name { get; set; }

        public List<User> Members { get; } = new List<User>();
    }
}
