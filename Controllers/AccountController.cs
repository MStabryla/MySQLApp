using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySQLApp.Models;
using Microsoft.AspNet.Identity.Owin;
using MySQLApp.Repo;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MySQLApp.Controllers
{
    [Authorize]
    [RoutePrefix("konto")]
    [Route("{action=Login}")]
    public class AccountController : Controller
    {
        // GET: Account
        private IAuth Auth;

        public AccountController()
        {
        }
        public AccountController(IAuth auth)
        {
            Auth = auth;
        }
        private MyUser ActUser
        {
            get
            {
                return Auth.GetUser(User.Identity.Name);
            }
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("zaloguj")]
        public ActionResult Login(string ReturnUrl = "")
        {
            if(ReturnUrl != "")
            {
                ViewBag.rUrl = ReturnUrl;
            }
            return View();
        }
        [HttpPost]
        [Route("zaloguj")]
        [AllowAnonymous]
        public ActionResult Login(Login_View model,string ReturnUrl)
        {
            if(ModelState.IsValid)
            {
                if(Auth.SingIn(model).Result)
                {
                    return Redirect(ReturnUrl != "" ? ReturnUrl : "user");
                }
                else
                {
                    ViewBag.LoginError = "Login lub hasło niepoprawne";
                }
            }
            return View();
        }
        [Route("user")]
        public ActionResult GetUser()
        {
            return View(ActUser);
        }
        [Route("user/{name}")]
        [AllowAnonymous]
        public ActionResult GetUser(string name)
        {
            return View(ActUser);
        }
        [HttpGet]
        [Route("profil")]
        public ActionResult EditProfile()
        {
            return View("EditUser", ActUser);
        }
        [HttpPost]
        [Route("profil")]
        public async Task<ActionResult> EditProfile(EditUser_View model)
        {
            MyUser actUser = ActUser;
            actUser.Email = model.Email;
            actUser.UserName = model.UserName;
            if (await Auth.EditUser(actUser))
            {
                ViewBag.Completed = true;
            }
            else
            {
                ViewBag.Completed = false;
            }
            return View(ActUser);
        }
        [HttpGet]
        [Route("edytuj/{name}")]
        [Authorize(Roles = "admin")]
        public ActionResult EditUser(string name)
        {
            return View(Auth.GetUser(name));
        }
        [HttpPost]
        [Route("edytuj")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> EditUser(EditUser_View model)
        {
            MyUser actUser = Auth.GetUserById(model.Id);
            actUser.UserName = model.UserName;
            actUser.Email = model.Email;
            if (await Auth.EditUser(actUser))
            {
                ViewBag.Completed = true;
            }
            else
            {
                ViewBag.Completed = false;
            }
            return RedirectToAction("GetUser");
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("rejestracja")]
        public ActionResult Registry()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("rejestracja")]
        public async Task<ActionResult> Registry(CreateUser model)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.Message = "Niepoprawne dane";
                return View();
            }
            if(model.Password != model.PasswordConfirm)
            {
                ViewBag.Message = "Hasła nie są podobne";
                return View();
            }
            MyUser nUser = new MyUser
            {
                Email = model.Email,
                UserName = model.UserName
            };
            if(await Auth.AddUser(nUser,model.Password))
            {
                ViewBag.Completed = true;
            }
            else
            {
                ViewBag.Completed = false;
            }
            return View();
        }
        [Authorize(Roles = "admin")]
        [Route("użytkownicy")]
        public ActionResult UserPanel()
        {
            return View(Auth.GetUsers().ToList());
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        [Route("usun")]
        public async Task<ActionResult> DeleteUser(string Name)
        {
            MyUser deletedUser = Auth.GetUser(Name);
            dynamic obj = new { };
            if(await Auth.RemoveUser(deletedUser))
            {
                obj.Completed = true;
            }
            else
            {
                obj.Completed = false;
            }
            return Json(obj);
        }
        [Route("wyloguj")]
        public ActionResult LogOut()
        {
            Auth.SingOut();
            return Redirect("/");
        }
    }
}