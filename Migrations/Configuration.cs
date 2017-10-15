namespace MySQLApp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using MySQLApp.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;

    internal sealed class Configuration : DbMigrationsConfiguration<MySQLApp.Models.ModelsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MySQLApp.Models.ModelsContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            
            AddRoles(context);
            AddUsers(context);
        }
        private void AddRoles(ModelsContext context)
        {
            var uMan = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            string[] roles = { "admin","worker", "user" };
            foreach(string role in roles)
            {
                if(!uMan.RoleExists(role))
                {
                    var nrole = new IdentityRole(role);
                    uMan.Create(nrole);
                    
                }
            }
        }
        private void AddUsers(ModelsContext context)
        {
            var users = new[]
            {
                new {Name = "admin", Role = "admin", Email = "admin@shop.pl"},
                new {Name = "worker", Role = "worker", Email = "worker@shop.pl"},
                new {Name = "user", Role = "user", Email = "user@shop.pl"}
            };
            var uMan = new MyUserManager(new UserStore<MyUser>(context));
            if(uMan.Users.Count() == 0)
            {
                foreach(var user in users)
                {
                    var nUser = new MyUser() { UserName = user.Name, Email = user.Email };
                    var result = uMan.Create(nUser, nUser.UserName);
                    if (result.Succeeded)
                    {
                        var res = uMan.AddToRole(nUser.Id, user.Role);
                        if(!res.Succeeded)
                        {
                            throw new Exception("Nie uda³o siê dodaæ usera do roli");
                        }
                    }
                    else
                    {
                        var errors = "";
                        foreach(string error in result.Errors)
                        {
                            errors += error + "\n";
                        }
                        throw new Exception("Nie uda³o siê dodaæ users\n" + errors);
                    }
                }
            }
        }
    }
}
