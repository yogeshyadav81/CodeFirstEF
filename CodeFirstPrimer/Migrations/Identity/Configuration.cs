namespace CodeFirstPrimer.Migrations.Identity
{
    using System;
    using CodeFirstPrimer.Models;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<CodeFirstPrimer.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\Identity";
        }

        protected override void Seed(CodeFirstPrimer.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (!roleManager.RoleExists("Admin"))
                roleManager.Create(new IdentityRole("Admin"));

            if (!roleManager.RoleExists("Guest"))
                roleManager.Create(new IdentityRole("Guest"));

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            string[] emails = { "admin@gmail.com","guest@gmail.com"};
            if (userManager.FindByEmail(emails[0])==null)
            {
                var user = new ApplicationUser()
                {
                    Email = emails[0],
                    UserName =emails[0]

                };
                var result = userManager.Create(user, "pass123");
                if (result.Succeeded)
                    userManager.AddToRole(userManager.FindByEmail(user.Email).Id, "Admin");
              
            }

            if (userManager.FindByEmail(emails[1]) == null)
            {
                var user = new ApplicationUser()
                {
                    Email = emails[1],
                    UserName = emails[1]

                };
                var result = userManager.Create(user, "pass123");
                if (result.Succeeded)
                   userManager.AddToRole(userManager.FindByEmail(user.Email).Id, "Guest");
                
            }
        }
    }
}
