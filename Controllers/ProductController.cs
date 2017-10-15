using MySQLApp.Models;
using MySQLApp.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MySQLApp.Controllers
{
    public class ProductController : Controller
    {
        private IData db;
        // GET: Main
        public ProductController(IData data)
        {
            db = data;
        }
        [Route]
        public ActionResult Products()
        {
            List<Product> test = db.GetProducts().ToList();
            return View(db.GetProducts());
        }
        [Route("get/{Id}")]
        public ActionResult Product(int Id)
        {
            return View(db.GetProduct(Id));
        }
        [HttpGet]
        [Route("nowy")]
        [Authorize(Roles = "admin,worker")]
        public ActionResult CreateProduct()
        {
            return View();
        }
        [HttpPost]
        [Route("nowy")]
        [Authorize(Roles = "admin,worker")]
        public async Task<ActionResult> CreateProduct(CreateProduct model,HttpPostedFileBase image)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.Message = "Niepoprawny model danych";
                return View();
            }
            Product newP = new Product();
            newP.Name = model.Name;
            newP.Desc = model.Desc;
            newP.Price = model.Price;
            newP.Rating = model.Rating;
            if(image != null)
            {
                Image img = new Image();
                img.Name = image.FileName;
                img.Bytes = new byte[image.ContentLength];
                image.InputStream.Read(img.Bytes, 0, image.ContentLength);
                img.ConType = image.ContentType;

                if (! await db.CreateProduct(newP) && ! await db.CreateImage(img))
                {
                    ViewBag.Completed = false;
                    return View();
                }
                newP.Image = img;
                db.SetModify();
            }
            else if(!await db.CreateProduct(newP))
            {
                ViewBag.Completed = false;
                ViewBag.Message = "Error: nie udało się utworzyć produktu";
                return View();
            }
            ViewBag.Completed = true;
            return Redirect("get/" + newP.Id);
        }
        [HttpPost]
        [Route("setObraz")]
        [Authorize(Roles = "admin,worker")]
        public async Task<ActionResult> SetImage(int Id,HttpPostedFileBase image)
        {
            if(image != null)
            {
                Product pr = db.GetProduct(Id);
                Image img = new Image();
                img.Name = image.FileName;
                img.Bytes = new byte[image.ContentLength];
                image.InputStream.Read(img.Bytes, 0, image.ContentLength);
                img.ConType = image.ContentType;
                pr.Image = img;
                if(! await db.CreateImage(img) || ! await db.EditProduct(pr))
                {
                    ViewBag.Completed = false;
                    return View();
                }
            }
            ViewBag.Error = "Brak pliku";
            return View();
        }
        [Route("obraz/{id}")]
        public ActionResult GetImage(int id)
        {
            Image img = db.GetImage(id);
            if(img != null)
            {
                return File(img.Bytes, img.ConType);
            }
            else
            {
                return HttpNotFound("Error: image with id " + id + " not found");
            }
        }
        [HttpGet]
        [Route("edytuj/{id}")]
        public ActionResult EditProduct(int id)
        {
            Product aP = db.GetProduct(id);
            if(aP != null)
            {
                return View(aP);
            }
            else
            {
                return HttpNotFound();
            }
        }
        [HttpPost]
        [Route("edytuj")]
        public ActionResult EditProduct()
        {
            return View();
        }
    }
}