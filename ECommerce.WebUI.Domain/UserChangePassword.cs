using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.WebUI.Domain
{
    public class UserChangePassword
    {
        [Required(ErrorMessage = "{0} Your Mail is required")]
        [StringLength(35, MinimumLength = 3, ErrorMessage = "{0} Mail 3 karakterden kısa olamaz. 35 Harften büyük olamaz!")]
        [Compare("Email", ErrorMessage = "The email addresses do not match.")]
        [Display(Name = "Confirmation Email")]
        public string Email { get; set; }
        [Required(ErrorMessage ="{0} Your old password is required")]
        public string OldPassword { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])[\w~@#$%^&*+=`|{}:;!.?\'""()\[\]-]{8,30}$", ErrorMessage = "{0} Password cannot be less than 8 characters. It must contain at least one lowercase / uppercase letter and number!")]
        public string NewPassword { get; set; }
        [Required]
        [Compare("NewPassword", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string ConfirmPassword { get; set; }
    }
}
