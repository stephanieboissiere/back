using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMO_4.Models
{
    public class Log
    {
        public int Id { get; set; }
       
        public DateTime date_heure { get; set; }
        public string description { get; set; } = null!;
        public int codeMessage { get; set; }
        public int criticity { get; set; }
        public string type { get; set; }
        public string dump { get; set; }
        public int produitId { get; set; }
        public Produit produit { get; set; }
        public int userId { get; set; }
        public User user { get; set; }
        

    }
}
