using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BOL
{
    //public class ValidateFileAttribute : ValidationAttribute
    //{
    //public override bool IsValid(object value)
    //{
    //    bool IsValid = true;
    //    var file = value as HttpPostedFileBase;
    //    if (file == null || file.ContentLength > 2 * 1024 * 1024)
    //    {
    //        IsValid = false;
    //    }
    //    return IsValid;
    //}
    //}
    public class ShowUserDetails
    {
        public UpdateUserDetail updateUserDetail { get; set; }
        public UpdateUser updateUser  { get; set; }
    }
    public class UpdateUserDetail
    {
        public int UserDetailId { get; set; }
        public string Address { get; set; }
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Please upload image.")]
        //[ValidateFile(ErrorMessage = "Please select image smaller than 2 MB")]
        public HttpPostedFileBase Image { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select DOB")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DOB { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select gender")]
        public Nullable<int> GenderId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select country")]
        public Nullable<int> CountryId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select state")]
        public Nullable<int> StateId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select city")]
        public string LandMark { get; set; }
        public Nullable<int> CityId { get; set; }
        public string Phone { get; set; }
        public string PostalCode { get; set; }
        public UpdateUser updateUser { get; set; }
        public bool IsEdit { get; set; }

    }
    public class UpdateUser
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter name.")]
        public string Name { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter email id.")]
        [EmailAddress(ErrorMessage = "Enter valid email id.")]
        public string Email { get; set; }
    }
}
