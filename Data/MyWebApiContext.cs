
using AMO_4.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace AMO_4.Data
{
    public class MyWebApiContext: DbContext
    {
        
        public MyWebApiContext(DbContextOptions<MyWebApiContext> options) : base(options) {
            
        }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Produit> Produits { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
       


       


    }
}

