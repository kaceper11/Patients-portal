using System.Security.Cryptography.X509Certificates;
using PatientsPortalModel.Entities;

namespace PatientsPortalModel.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PatientsPortalModel.PatientsPortalContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PatientsPortalModel.PatientsPortalContext context)
        {
             context.Roles.AddOrUpdate(x => x.RoleId,
                new Role()
                {
                    RoleId = 1,
                    RoleName = "Administrator"
                },
                new Role()
                {
                    RoleId = 2,
                    RoleName = "Registration"
                },
                new Role()
                {
                    RoleId = 3,
                    RoleName = "Doctor"
                },
                new Role()
                {
                    RoleId = 4,
                    RoleName = "Patient"
                }
            );

            //context.Users.AddOrUpdate(new User()
            //{
            //    UserId = 15,
            //    UserName = "admin",
            //    Email = "mail@mail.com",
            //    Password = "password",
            //    FirstName = "Admin",
            //    LastName = "Admin",
            //    Gender = "Mê¿czyzna",
            //    MobileNumber = "123456789",
            //    Pesel = "12345678901",
            //    IsActive = true,
            //    CreatedBy = "seed-method",
            //    CreatedDate = DateTime.Now,
            //    ModifiedDate = null,
            //    ModifiedBy = null,
            //    TermsApprovedDate = DateTime.Now,
            //    TermsApproved = true
            //});

            //context.UserRoles.Add(new UserRole()
            //{
            //    UserId = 1,
            //    RoleId = 1
            //});
        }
    }
}
