using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMO_4.Models
{
    public class Role
    {
        //internal int userId;

        public int roleId { get; set; }
        public string name { get; set; }
        public string color { get; set; }
        public string description { get; set; }

        public ICollection<User> User { get; set; }
        
        

    }
}
