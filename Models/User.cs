using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMO_4.Models
{
    public class User
    {
        public int userId{ get; set; }
        public string ref_user { get; set; }
        public string name { get; set; }

        public string password { get; set; }
        //token ?
        public ICollection<Role> Role { get; set; }
       
        //public ICollection<Log> Logs { get; set; }
    }
    public class AuthenticatedResponse
    {
        public string? Token { get; set; }
    }
}
