using Gharbhetibaa.Models.Item;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Gharbhetibaa.Models.Booking
{
    public class BookingItem
    {
        [Key]
        public int BookingID { get; set; }

        public int ItemID { get; set; }

        public int BookedBy { get; set; }

        [ForeignKey("ItemID")]
        public virtual ItemDetail ItemDetail { get; set; }

        [ForeignKey("BookedBy")]
        public virtual UserAccount UserAccount { get; set; }
    }
}