using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MySQLApp.Models
{
    public class Product
    {
        public Product()
        {
            Orders = new HashSet<Order>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Desc { get; set; }
        public double Rating { get; set; }
        public double Price { get; set; }
        public Image Image { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}