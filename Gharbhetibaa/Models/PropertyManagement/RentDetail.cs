using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gharbhetibaa.Models.PropertyManagement
{
    public class RentDetail
    {
        [Key]
        public int RentID { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Month { get; set; }

        [Display(Name = "Monthly Rent")]
        public Single? MonthlyRent { get; set; }

        [Display(Name = "Electricity")]
        public Single? Electricity { get; set; }

        [Display(Name = "Water Supply")]
        public Single? WaterSupply { get; set; }

        [Display(Name = "Miscellaneous")]
        public Single? Miscellaneous { get; set; }

        [Display(Name = "Total")]
        public double? Total
        {
            get
            {
                return (MonthlyRent + Electricity + WaterSupply + Miscellaneous);
            }
        }

        [Display(Name = "Amount Payed")]
        public Single? AmountPayed { get; set; }

        public virtual ICollection<UserRentDetail> UserRentDetails { get; set; }
    }
}