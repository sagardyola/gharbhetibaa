using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gharbhetibaa.Models
{
    public class Newsletter
    {
        private DateTime _date = DateTime.Now;

        [Key]
        public int NewsletterID { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "You need to enter your email address.")]
        [DataType(DataType.EmailAddress)]
        public string EmailID { get; set; }

        [Display(Name = "Contacted On")]
        public DateTime Timestamp
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
            }
        }
    }
}