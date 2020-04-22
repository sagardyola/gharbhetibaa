using Gharbhetibaa.Models.Item;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Gharbhetibaa.Models.WishList
{
    public class WishListItem
    {
        [Key]
        public int WishListID { get; set; }

        public int ItemID { get; set; }

        public int WishListedBy { get; set; }

        [ForeignKey("ItemID")]
        public virtual ItemDetail ItemDetail { get; set; }

        [ForeignKey("WishListedBy")]
        public virtual UserAccount UserAccount { get; set; }
    }
}