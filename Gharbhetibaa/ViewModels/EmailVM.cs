using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gharbhetibaa.ViewModels
{
    public class EmailVM
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please enter email address.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Subject")]
        [Required(ErrorMessage = "Please enter subject.")]
        public string Subject { get; set; }

        [Display(Name = "Message")]
        [Required(ErrorMessage = "Please write your message.")]
        public string Message { get; set; }
    }
}