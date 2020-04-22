using Gharbhetibaa.Models.SearchingForItem;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Gharbhetibaa.Models.WishList
{
    public class WishListSearching
    {
        [Key]
        public int WishListID { get; set; }

        public int SearchingForID { get; set; }

        public int WishListedBy { get; set; }

        [ForeignKey("SearchingForID")]
        public virtual SearchingFor SearchingFor { get; set; }

        [ForeignKey("WishListedBy")]
        public virtual UserAccount UserAccount { get; set; }
    }
}