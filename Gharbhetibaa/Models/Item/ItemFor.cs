using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gharbhetibaa.Models.Item
{
    public class ItemFor
    {
        [Key]
        public int ItemForID { get; set; }

        [Display(Name = "For")]
        public string ItemForTitle { get; set; }
    }
}