using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gharbhetibaa.Models.Item
{
    public class ItemType
    {
        [Key]
        public int ItemTypeID { get; set; }

        [Display(Name = "Type")]
        public string ItemTypeTitle { get; set; }
    }
}