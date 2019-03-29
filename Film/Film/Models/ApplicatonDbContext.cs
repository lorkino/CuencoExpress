using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Film.Models
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {

       
        public DbSet<UserDates> UserDates { get; set; }
        public DbSet<Knowledges> Knowledges { get; set; }
        public DbSet<Job> Job { get; set; }
        public DbSet<Images> Images { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {

        }
       
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           // modelBuilder.Entity<User>().HasOne(b => b.UserDates).WithOne(b => b.User).OnDelete(DeleteBehavior.Restrict).HasForeignKey<UserDates>(b => b.Id);
            //modelBuilder.Entity<Trabajo>().HasOne(b => b.Cliente).WithOne(b => b.Trabajo).OnDelete(DeleteBehavior.Restrict).HasForeignKey<Cliente>(b => b.Id);
            
            //modelBuilder.Entity<Trabajo>().HasOne(b => b.Trabajador).WithOne(b => b.Trabajo).OnDelete(DeleteBehavior.Restrict).HasForeignKey<Trabajo>(b => b.Id);
            //modelBuilder.Entity<Trabajador>().HasOne(b => b.Trabajo).WithOne(b => b.Trabajador).OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<Cliente>().HasOne(b => b.Trabajo).WithOne(b => b.Cliente).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserKnowledges>()
           .HasKey(t => new { t.UserId, t.KnowledgesId });

            modelBuilder.Entity<UserKnowledges>()
                .HasOne(pt => pt.User)
                .WithMany(p => p.UserKnowledges)
                .HasForeignKey(pt => pt.UserId);

            modelBuilder.Entity<UserKnowledges>()
                .HasOne(pt => pt.Knowledges)
                .WithMany(t => t.UserKnowledges)
                .HasForeignKey(pt => pt.KnowledgesId);

            modelBuilder.Entity<JobKnowledges>()
          .HasKey(t => new { t.JobId, t.KnowledgesId });

            modelBuilder.Entity<JobKnowledges>()
                .HasOne(pt => pt.Job)
                .WithMany(p => p.JobKnowledges)
                .HasForeignKey(pt => pt.JobId);

            modelBuilder.Entity<JobKnowledges>()
                .HasOne(pt => pt.Knowledges)
                .WithMany(t => t.JobKnowledges)
                .HasForeignKey(pt => pt.KnowledgesId);

        }
    }
}
