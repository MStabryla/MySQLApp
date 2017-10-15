using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MySQLApp.Models
{
    public class CreateUser
    {
        [Required]
        [StringLength(128)]
        public string UserName { get; set; }
        [Required]
        [StringLength(128)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [StringLength(128)]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
        [Required]
        [StringLength(128)]
        public string Email { get; set; }
    }
}