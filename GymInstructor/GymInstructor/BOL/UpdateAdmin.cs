using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BOL
{
    public class UpdateAdmin
    {
        public UpdateAdminDetail updateAdminDetail   { get; set; }
    }
    public class UpdateAdminDetail
    {
        public int AdminId { get; set; }
        [Required(AllowEmptyStrings =false,ErrorMessage ="Please enter name.")]
        public string Name { get; set; }
        [Required(AllowEmptyStrings =false,ErrorMessage ="Please enter email")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(AllowEmptyStrings =false,ErrorMessage ="Please enter phone number")]
        public string Phone { get; set; }
        [Required(AllowEmptyStrings =false,ErrorMessage ="Please select DOB")]
        public DateTime DOB { get; set; }
        [Required(AllowEmptyStrings =false,ErrorMessage ="Please select gender")]
        public string Gender { get; set; }
        public string ImageName { get; set; }
        public string ImageType { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public bool IsEditAllow { get; set; }
    }
    [MetadataType(typeof(UpdateAdminDetail))]
    public partial class Admin
    {

    }

}
