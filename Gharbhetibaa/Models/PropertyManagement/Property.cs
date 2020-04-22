using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gharbhetibaa.Models.PropertyManagement
{
    public class Property
    {
        [Key]
        public int PropertyID { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "Please choose the title")]
        public string Title { get; set; }

        [Display(Name = "Location")]
        [Required(ErrorMessage = "Please specify the location")]
        public string Location { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "Please specify the city")]
        public string City { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Location")]
        public string LocationCity
        {
            get
            {
                return Location + ", " + City;
            }
        }

        public virtual ICollection<UserProperty> UserProperties { get; set; }
        public virtual ICollection<UserTenant> UserTenants { get; set; }
        public virtual ICollection<UserRentDetail> UserRentDetails { get; set; }

        public virtual ICollection<UserExpense> UserExpenses { get; set; }

    }
}