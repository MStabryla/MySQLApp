using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MySql.Data.Entity;
using System.Data.Common;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MySQLApp.Models
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class ModelsContext : IdentityDbContext<MyUser>
    {
        public ModelsContext() : base("MainCon")
        {

        }
        public static ModelsContext Create()
        {
            return new ModelsContext();
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Image> Images { get; set; }
    }
    public class MyUserManager : UserManager<MyUser>
    {
        public MyUserManager(IUserStore<MyUser> users) : base(users)
        {
            this.UserValidator = new UserValidator<MyUser>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            this.PasswordValidator = new PasswordValidator
            {
            };

            // Configure user lockout defaults
            this.UserLockoutEnabledByDefault = true;
            this.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(15);
            this.MaxFailedAccessAttemptsBeforeLockout = 8;
            // You can write your own provider and plug it in here.
        }
        public static MyUserManager Create(IdentityFactoryOptions<MyUserManager> options, IOwinContext context)
        {
            return new MyUserManager(new UserStore<MyUser>(context.Get<ModelsContext>()));
        }
    }
    public class MySignInManager : SignInManager<MyUser,string>
    {
        public MySignInManager(MyUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }
        public override Task<ClaimsIdentity> CreateUserIdentityAsync(MyUser user)
        {
            return user.GenerateUserIdentityAsync((MyUserManager)UserManager);
        }
        public static MySignInManager Create(IdentityFactoryOptions<MySignInManager> options, IOwinContext context)
        {
            return new MySignInManager(context.GetUserManager<MyUserManager>(),context.Authentication);
        }
    }
}