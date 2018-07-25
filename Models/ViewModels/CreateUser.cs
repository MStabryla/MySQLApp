using System.ComponentModel.DataAnnotations;

namespace MySQLApp.Models
{
    public class CreateUser
    {
        [Required]
        [StringLength(128)]
        [Display(Name = "Nazwa użytkownika")]
        public string UserName { get; set; }
        [Required]
        [StringLength(128)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }
        [Required]
        [StringLength(128)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "Powtórz Hasło")]
        public string PasswordConfirm { get; set; }
        [Required]
        [StringLength(128)]
        public string Email { get; set; }
    }
}