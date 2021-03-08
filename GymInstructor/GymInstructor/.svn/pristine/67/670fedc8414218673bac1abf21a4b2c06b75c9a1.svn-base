using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    public class UniqueEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext  validationContext)
        {
            MVCDemoEntities mVCDemoEntities = new MVCDemoEntities();
            string userEmail = value.ToString();
            var checkEmail = (from o in mVCDemoEntities.Users where o.Email == userEmail select o).FirstOrDefault();
            if (checkEmail!=null)
            {
                return new ValidationResult("Email id already taken please try different email.");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
    public class UserValidation
    {
        [Required(AllowEmptyStrings =false,ErrorMessage ="Please enter name.")]
        [MaxLength(100,ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }
        [Required(AllowEmptyStrings =false,ErrorMessage ="Please enter email id.")]
        [EmailAddress(ErrorMessage ="Enter valid email id.")]
        [UniqueEmail]
        [MaxLength(100,ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Email { get; set; }
        [Required(AllowEmptyStrings =false,ErrorMessage ="Please enter password.")]
        [MaxLength(100,ErrorMessage = "Passwprd cannot be longer than 100 characters.")]
        public string Password { get; set; }
        [Required(AllowEmptyStrings =false,ErrorMessage ="Please enter confirm password .")]
        [Compare("Password",ErrorMessage ="Password and confirm password does not match")]
        [MaxLength(100,ErrorMessage = "Confirm password cannot be longer than 100 characters.")]
        public string ConfirmPassword { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
    }
    [MetadataType(typeof(UserValidation))]
    public partial class User
    {
        public string ConfirmPassword { get; set; }
    }
}
