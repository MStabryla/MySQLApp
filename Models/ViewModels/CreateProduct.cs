using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MySQLApp.Models
{
    public class CreateProduct
    {
        [Required]
        public string Name { get; set; }
        public string Desc { get; set; }
        public double Rating { get; set; }
        public double Price { get; set; }
    }
}