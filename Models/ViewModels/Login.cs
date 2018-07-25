using System.ComponentModel.DataAnnotations;

namespace MySQLApp.Models
{
    public class Login_View
    {
        [Required]
        public string Login { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsPersistent { get; set; }
    }
}