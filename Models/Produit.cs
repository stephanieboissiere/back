using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMO_4.Models
{
    public class Produit
    {
        public int Id { get; set; }
        public string ref_produit { get; set; }
        public string version { get; set; }
        public string name { get; set; }
        
        //public ICollection<Log> Logs { get; set; }

    }
}
