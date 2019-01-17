using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Film.Models
{
    public class ApplicatonDbContext : IdentityDbContext<User>
    {

        
        public DbSet<Trabajador> Trabajador { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Trabajo> Trabajo { get; set; }
        public DbSet<UserDates> UserDates { get; set; }
       

        public ApplicatonDbContext(DbContextOptions<ApplicatonDbContext> options)
           : base(options)
        {

        }
       
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasOne(b => b.UserDates).WithOne(b => b.User).OnDelete(DeleteBehavior.Restrict).HasForeignKey<UserDates>(b => b.Id);
            modelBuilder.Entity<Trabajo>().HasOne(b => b.Cliente).WithOne(b => b.Trabajo).OnDelete(DeleteBehavior.Restrict).HasForeignKey<Cliente>(b => b.Id);
           
            //modelBuilder.Entity<Trabajo>().HasOne(b => b.Trabajador).WithOne(b => b.Trabajo).OnDelete(DeleteBehavior.Restrict).HasForeignKey<Trabajo>(b => b.Id);
            //modelBuilder.Entity<Trabajador>().HasOne(b => b.Trabajo).WithOne(b => b.Trabajador).OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<Cliente>().HasOne(b => b.Trabajo).WithOne(b => b.Cliente).OnDelete(DeleteBehavior.Restrict);



        }
    }
}
