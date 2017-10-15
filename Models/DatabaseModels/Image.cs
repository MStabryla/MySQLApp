using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MySQLApp.Models
{
    public class Image
    {
        public Image()
        {
            Products = new HashSet<Product>();
        }
        [Key]
        public int Id { get; set; }
        public byte[] Bytes { get; set; }
        public string Name { get; set; }
        public string ConType { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}