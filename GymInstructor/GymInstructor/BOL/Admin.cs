//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BOL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Admin
    {
        public int AdminId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public System.DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string ImageName { get; set; }
        public string ImageType { get; set; }
        public byte[] Image { get; set; }
        public bool Status { get; set; }
    }
}