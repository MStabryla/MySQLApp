using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySQLApp.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace MySQLApp.Repo
{
    public class Auth : IAuth
    {
        private MySignInManager sMan { get; set; }
        private MyUserManager uMan { get; set; }
        public Auth(MySignInManager _sMan,MyUserManager _uMan)
        {
            sMan = _sMan;
            uMan = _uMan;
        }
        MyUser IAuth.GetUser(string Name)
        {
            return uMan.Users.First(x => x.UserName == Name);
        }
        MyUser IAuth.GetUserById(string Id)
        {
            return uMan.Users.First(x => x.Id == Id);
        }
        IQueryable<MyUser> IAuth.GetUsersInRoles(string Role)
        {
            return uMan.Users.Where(x => uMan.IsInRoleAsync(x.Id.ToString(), Role).Result);
        }
        IQueryable<MyUser> IAuth.GetUsers()
        {
            return uMan.Users;
        }
        async Task<bool> IAuth.AddUser(MyUser user,string password)
        {
            var result = await uMan.CreateAsync(user,password);
            return result.Succeeded;
        }
        async Task<bool> IAuth.RemoveUser(MyUser user)
        {
            var result = await uMan.DeleteAsync(user);
            return result.Succeeded;
        }
        async Task<bool> IAuth.EditUser(MyUser user)
        {
            var result = await uMan.UpdateAsync(user);
            return result.Succeeded;
        }
        async Task<bool>  IAuth.SingIn(Login_View model)
        {
            var result = await sMan.PasswordSignInAsync(model.Login, model.Password, model.IsPersistent, false);
            return result == Microsoft.AspNet.Identity.Owin.SignInStatus.Success;
        }
        void IAuth.SingOut()
        {
            sMan.AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }
    }
}