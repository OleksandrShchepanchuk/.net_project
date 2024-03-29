using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class HospitalContext : DbContext
    {
        public HospitalContext(DbContextOptions<HospitalContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Wish> Wishes { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Wish>()
                .HasOne(u => u.User)
                .WithMany(w => w.Wishes)
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<Review>()
                .HasOne(u => u.User)
                .WithMany(r => r.Reviews)
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Department)
                .WithMany(doc => doc.Doctors)
                .HasForeignKey(d => d.DepartmentId);
        }
    }
}
