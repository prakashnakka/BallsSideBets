using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BL.Models
{
    public class UserAccount
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required"), DisplayName("Password")]
        public string Pwd { get; set; }
        [Required(ErrorMessage = "Display name is required"), DisplayName("Display Name")]
        public string DisplayName { get; set; } = "default";
        public string IsAdmin { get; set; }
    }

    public class ChangePassword
    {
        [DataType(DataType.Password), DisplayName("Current Password")]
        public string CurrentPassword { get; set; }
        [DataType(DataType.Password), MinLength(5), DisplayName("New Password")]
        public string NewPassword { get; set; }
        [DataType(DataType.Password), MinLength(5)]
        [Compare("NewPassword"), DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
