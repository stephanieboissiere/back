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
        public string username { get; set; }

        public string password { get; set; }
        public string? RefreshToken { get; set; }//ici
        public DateTime RefreshTokenExpiryTime { get; set; }//ici

        public ICollection<Role> Role { get; set; }
       
        
    }
    public class AuthenticatedResponse//modifier en interface
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }//ici
        
       //public string name { get; set; }

       // public string role { get; set; }
        


    }
}
