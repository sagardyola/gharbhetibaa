using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gharbhetibaa.Models
{
    public class Feedback
    {
        private DateTime _date = DateTime.Now;

        [Key]
        public int FeedbackID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Subject")]
        [Required(ErrorMessage = "This field is required.")]
        public string Subject { get; set; }

        [Display(Name = "Message")]
        [Required(ErrorMessage = "Let Us Know What You Think. We'd love to hear from you.")]
        public string Message { get; set; }

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