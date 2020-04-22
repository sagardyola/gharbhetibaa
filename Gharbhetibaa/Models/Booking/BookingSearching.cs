using Gharbhetibaa.Models.SearchingForItem;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Gharbhetibaa.Models.Booking
{
    public class BookingSearching
    {
        [Key]
        public int BookingID { get; set; }

        public int SearchingForID { get; set; }

        public int BookedBy { get; set; }

        [ForeignKey("SearchingForID")]
        public virtual SearchingFor SearchingFor { get; set; }

        [ForeignKey("BookedBy")]
        public virtual UserAccount UserAccount { get; set; }
    }
}