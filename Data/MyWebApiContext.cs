
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
       


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
            //modelBuilder.Entity<User_Role>()
            //.HasKey(bc => new { bc.userID, bc.roleID });
            //modelBuilder.Entity<User_Role>()
            //    .HasOne(bc => bc.User)
            //    .WithMany(b => b.User_Role)
            //    .HasForeignKey(bc => bc.userID);
            //modelBuilder.Entity<User_Role>()
            //    .HasOne(bc => bc.Role)
            //    .WithMany(c => c.User_Role)
            //    .HasForeignKey(bc => bc.roleID);

           // modelBuilder.Entity<Log>()
           // .HasOne<Produit>(s => s.produit)
           // .WithMany(g => g.Logs)
           // .HasForeignKey(s => s.produitId);
           // modelBuilder.Entity<Log>()
           //.HasOne<User>(s => s.user)
           //.WithMany(g => g.Logs)
           //.HasForeignKey(s => s.userId);
       // }


    }
}

