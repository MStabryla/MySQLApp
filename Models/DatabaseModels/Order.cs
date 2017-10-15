using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MySQLApp.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public MyUser User { get; set; }
        public ICollection<Product> Products { get; set; }
        public DateTime Date { get; set; }
    }
}