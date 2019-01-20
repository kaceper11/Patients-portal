using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using PatientsPortalModel.Entities;
using TrackerEnabledDbContext;

namespace PatientsPortalModel
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class PatientsPortalContext : TrackerContext
    {
        // Your context has been configured to use a 'PatiensPortalContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'PatiensPortalModel.PatiensPortalContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'PatiensPortalContext' 
        // connection string in the application configuration file.
        public PatientsPortalContext()
            : base("name=PatientsPortal")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .Map(m =>
                {
                    m.ToTable("UserRoles");
                    m.MapLeftKey("UserId");
                    m.MapRightKey("RoleId");
                });
        }



        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<Examination> Examinations { get; set; }

        public virtual DbSet<Doctor> Doctors { get; set; }

        public virtual DbSet<Problem> Problems { get; set; }

        public virtual DbSet<Unit> Units { get; set; }

        public virtual DbSet<Visit> Visits { get; set; }

        public virtual DbSet<Term> Terms { get; set; }

        public virtual DbSet<WorkHour> WorkHours { get; set; }

        public virtual DbSet<ExaminationFile> ExaminationFiles { get; set; }

        public virtual DbSet<Conversation> Conversations { get; set; }
    }
}