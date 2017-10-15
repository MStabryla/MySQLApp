using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySQLApp.Models;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MySQLApp.Repo
{
    public class Data : IData
    {
        private ModelsContext db { get; set; }
        public Data(ModelsContext context)
        {
            db = context;
        }
        IQueryable<Product> IData.GetProducts()
        {
            return db.Products.Include(x => x.Image);
        }
        Product IData.GetProduct(int id)
        {
            return db.Products.Include(x => x.Image).First(x => x.Id == id);
        }
        async Task<bool> IData.CreateProduct(Product model)
        {
            db.Products.Add(model);
            try
            {
                int result = await db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        async Task<bool> IData.EditProduct(Product model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            try
            {
                int result = await db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        async Task<bool> IData.RemoveProduct(Product model)
        {
            db.Products.Remove(db.Products.First(x => x.Id == model.Id));
            try
            {
                int result = await db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        Image IData.GetImage(int id)
        {
            return db.Images.First(x => x.Id == id);
        }
        async Task<bool> IData.CreateImage(Image model)
        {
            db.Images.Add(model);
            try
            {
                int result = await db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        async Task<bool> IData.EditImage(Image model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            try
            {
                int result = await db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        async Task<bool> IData.RemoveImage(Image model)
        {
            db.Images.Remove(db.Images.First(x => x.Id == model.Id));
            try
            {
                int result = await db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        async void IData.SetModify()
        {
            await db.SaveChangesAsync();
        }
    }
}