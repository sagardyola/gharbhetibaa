using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Gharbhetibaa.Models.PropertyManagement
{
    public class Expense
    {
        [Key]
        public int ExpenseID { get; set; }

        [Required(ErrorMessage = "Please enter your expense name")]
        [Display(Name = "Title")]
        public string ExpenseTitle { get; set; }

        [Display(Name = "Total Amount")]
        public double TotalAmount { get; set; }

        [Display(Name = "Payment Date")]
        [Required(ErrorMessage = "Please enter the date of Payment")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PaymentDate { get; set; }

        [Display(Name = "Notes")]
        public string Notes { get; set; }

        public virtual ICollection<UserExpense> UserExpenses { get; set; }



    }
}