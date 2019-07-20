using EntityFrameworkCore.Triggers;
using Film.SignalR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Film.Models
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {


        public DbSet<UserDates> UserDates { get; set; }
        public DbSet<Knowledges> Knowledges { get; set; }
        public DbSet<Job> Job { get; set; }
        public DbSet<Images> Images { get; set; }
        public DbSet<JobPreWorker> JobPreWorkers { get; set; }
        public DbSet<Suscription> Suscriptions { get; set; }
        // public DbSet<JobPreWorker> JobPreWorker { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {

        }

        public override Int32 SaveChanges()
        {
          
            return this.SaveChangesWithTriggers(base.SaveChanges, acceptAllChangesOnSuccess: true);
        }
        public override Int32 SaveChanges(Boolean acceptAllChangesOnSuccess)
        {
           
            return this.SaveChangesWithTriggers(base.SaveChanges, acceptAllChangesOnSuccess);
        }
        public override Task<Int32> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            
            return this.SaveChangesWithTriggersAsync(base.SaveChangesAsync, acceptAllChangesOnSuccess: true, cancellationToken: cancellationToken);
        }
        public override Task<Int32> SaveChangesAsync(Boolean acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            
            return this.SaveChangesWithTriggersAsync(base.SaveChangesAsync, acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // modelBuilder.Entity<User>().HasOne(b => b.UserDates).WithOne(b => b.User).OnDelete(DeleteBehavior.Restrict).HasForeignKey<UserDates>(b => b.Id);
            //modelBuilder.Entity<Trabajo>().HasOne(b => b.Cliente).WithOne(b => b.Trabajo).OnDelete(DeleteBehavior.Restrict).HasForeignKey<Cliente>(b => b.Id);

            //modelBuilder.Entity<Trabajo>().HasOne(b => b.Trabajador).WithOne(b => b.Trabajo).OnDelete(DeleteBehavior.Restrict).HasForeignKey<Trabajo>(b => b.Id);
            //modelBuilder.Entity<Trabajador>().HasOne(b => b.Trabajo).WithOne(b => b.Trabajador).OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<Cliente>().HasOne(b => b.Trabajo).WithOne(b => b.Cliente).OnDelete(DeleteBehavior.Restrict);
            //many to many kownowledges con user
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
            //many to many kownowledges con jobs
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


            //relacion con jovs preworker,worker,etc

            //many to many preworker y jobs

            //modelBuilder.Entity<JobPreWorker>()
            //    .HasKey(bc => new { bc.JobId, bc.UserPreWorkeId });
            //modelBuilder.Entity<JobPreWorker>()
            //    .HasOne(bc => bc.UserPreWorker)
            //    .WithMany(b => b.JobsPreworker)
            //    .HasForeignKey(bc => bc.JobId);
            //modelBuilder.Entity<JobPreWorker>()
            //    .HasOne(bc => bc.Job)
            //    .WithMany(c => c.UserPreWorker)
            //    .HasForeignKey(bc => bc.UserPreWorkeId);


            //many to many job con preworker
            modelBuilder.Entity<JobPreWorker>()
          .HasKey(t => new { t.JobId, t.UserPreWorkeId });

            modelBuilder.Entity<JobPreWorker>()
                .HasOne(pt => pt.Job)
                .WithMany(p => p.UserPreWorker)
                .HasForeignKey(pt => pt.JobId);

            modelBuilder.Entity<JobPreWorker>()
                .HasOne(pt => pt.UserPreWorker)
                .WithMany(t => t.JobPreWorker)
                .HasForeignKey(pt => pt.UserPreWorkeId);

            modelBuilder.Entity<User>().HasKey(k => k.Id);
            modelBuilder.Entity<User>().HasMany(a => a.JobsCreator)
           .WithOne(a => a.UserCreator);
            modelBuilder.Entity<User>().HasMany(a => a.JobsWorker)
           .WithOne(a => a.UserWorker);




        }
    }
}
