using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    public class SingIn
    {
        public User user { get; set; }
        public Login login { get; set; }
        public ForgetPassword forgetPassword { get; set; }
        public ReSetPassword resetPassword { get; set; }
        public ChangePassword changePassword { get; set; }
    }
    public class Login
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter email id.")]
        [EmailAddress(ErrorMessage = "Enter valid email id.")]
        [MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter password.")]
        [MaxLength(100, ErrorMessage = "Passwprd cannot be longer than 100 characters.")]
        public string Password { get; set; }
    }
    public class ForgetPassword
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter email id.")]
        [EmailAddress(ErrorMessage = "Enter valid email id.")]
        [MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Email { get; set; }
    }
    public class ReSetPassword
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter password .")]
        public string Password { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter confirm password .")]
        [Compare("Password", ErrorMessage = "Password and confirm password does not match")]
        [MaxLength(100, ErrorMessage = "Confirm password cannot be longer than 100 characters.")]
        public string ConfirmPassword { get; set; }
    }
    public class ChangePassword
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please entercurrent password .")]
        public string CurrentPassword { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter password .")]
        public string Password { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter confirm password .")]
        [Compare("Password", ErrorMessage = "Password and confirm password does not match")]
        [MaxLength(100, ErrorMessage = "Confirm password cannot be longer than 100 characters.")]
        public string ConfirmPassword { get; set; }
    }
}
