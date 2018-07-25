using System.ComponentModel.DataAnnotations;

namespace MySQLApp.Models
{
    public class EditUser_View
    {
        [Required]
        public string Id { get; set; }
        [Required]
        [StringLength(128)]
        public string Email { get; set; }
        [Required]
        [StringLength(128)]
        public string UserName { get; set; }
    }
}