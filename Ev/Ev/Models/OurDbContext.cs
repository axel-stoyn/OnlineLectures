using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Ev.Models
{
    public class OurDbContext : DbContext
    {
        public DbSet<UserAccount> UserAccount { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Training> Training { get; set; }
        public DbSet<VirtualClass> VirtualClass { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Training>().
                HasMany(c => c.virtualClasses).
                WithMany(p => p.trainings).
                Map(
                m =>
                {
                    m.MapLeftKey("TrainingID");
                    m.MapRightKey("VirtualClassID");
                    m.ToTable("Enrollment");
                });
            modelBuilder.Entity<VirtualClass>().
                HasMany(v => v.students).
                WithMany(a => a.virtualClass).
                Map(
                m =>
                {
                    m.MapLeftKey("VirtualClassID");
                    m.MapRightKey("StudentID");
                    m.ToTable("Enrollmentss");
                });

            modelBuilder.Entity<UserAccount>().
                HasMany(r => r.roles).
                WithMany(u => u.userAccount).
                Map(
                m =>
                {
                    m.MapLeftKey("UserID");
                    m.MapRightKey("RoleID");
                    m.ToTable("UserRoles");
                });
                
        }

    }
}