using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MySQLApp.Models
{
    public class EditProduct
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(128)]
        public string Name { get; set; }
        public string Desc { get; set; }
        [Required]
        [Range(1,5)]
        public double Rating { get; set; }
        [Required]
        [Range(0,double.MaxValue)]
        public double Price { get; set; }
    }
}