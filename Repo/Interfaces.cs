using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySQLApp.Models;
using System.Threading.Tasks;

namespace MySQLApp.Repo
{
    public interface IAuth
    {
        MyUser GetUser(string Name);
        MyUser GetUserById(string Id);
        IQueryable<MyUser> GetUsersInRoles(string Role);
        IQueryable<MyUser> GetUsers();
        Task<bool> AddUser(MyUser user,string password);
        Task<bool> RemoveUser(MyUser user);
        Task<bool> EditUser(MyUser user);
        Task<bool> SingIn(Login_View model);
        void SingOut();
    }
    public interface IData
    {
        IQueryable<Product> GetProducts();
        Product GetProduct(int id);
        Task<bool> CreateProduct(Product model);
        Task<bool> EditProduct(Product model);
        Task<bool> RemoveProduct(Product model);
        Image GetImage(int id);
        Task<bool> CreateImage(Image img);
        Task<bool> EditImage(Image model);
        Task<bool> RemoveImage(Image model);
        void SetModify();

    }
}