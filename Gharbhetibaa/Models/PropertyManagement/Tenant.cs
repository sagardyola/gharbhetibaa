using Gharbhetibaa.Models.Images;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gharbhetibaa.Models.PropertyManagement
{
    public class Tenant
    {
        [Key]
        public int TenantID { get; set; }

        //Tenant Details
        [Required(ErrorMessage = "Please enter Tenants name")]
        [Display(Name = "Name")]
        public string TenantName { get; set; }

        [Required(ErrorMessage = "Please enter Tenants address")]
        [Display(Name = "Address")]
        public string TenantAddress { get; set; }

        [Display(Name = "Mobile")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string TenantMobile { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string TenantEmail { get; set; }

        [Display(Name = "Date Joined")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateJoined { get; set; }

        [Display(Name = "Nationality ID")]
        public string NationalityID { get; set; }

        [Display(Name = "Notes")]
        public string TenantNotes { get; set; }

        public virtual ICollection<UserTenant> UserTenants { get; set; }
        public virtual ICollection<UserRentDetail> UserRentDetails { get; set; }


        public virtual List<PictureContractDoc> PictureContractDocs { get; set; }

    }
}