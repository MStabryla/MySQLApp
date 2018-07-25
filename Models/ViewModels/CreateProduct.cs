using System.ComponentModel.DataAnnotations;

namespace MySQLApp.Models
{
    public class CreateProduct
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; }
        public string Desc { get; set; }
        [Range(1,5)]
        public int Rating { get; set; }
        [Range(0,double.MaxValue)]
        public double Price { get; set; }
    }
}