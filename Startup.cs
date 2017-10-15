using System;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Mvc;
using MySQLApp.Models;

[assembly: OwinStartup(typeof(MySQLApp.Startup))]

namespace MySQLApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            // Enable the application to use a cookie to store information for the signed in user
            //app.CreatePerOwinContext(ModelsContext.Create);
            //app.CreatePerOwinContext<MyUserManager>(MyUserManager.Create);
            //app.CreatePerOwinContext<MySignInManager>(MySignInManager.Create);
            app.CreatePerOwinContext(() => DependencyResolver.Current.GetService<MyUserManager>());
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/konto/zaloguj"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<MyUserManager, MyUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
        }
    }
}
