using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gharbhetibaa.Models.PropertyManagement
{
    public class Supplier
    {
        [Key]
        public int SupplierID { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please enter name of the supplier")]
        public string SupplierName { get; set; }

        [Display(Name = "Profession")]
        [Required(ErrorMessage = "Please specify profession")]
        public string Profession { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Display(Name = "Mobile")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string MobileNumber { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Notes")]
        public string SupplierNotes { get; set; }

        public virtual ICollection<UserSupplier> UserSuppliers { get; set; }
    }
}