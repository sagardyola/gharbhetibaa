using Gharbhetibaa.Models;
using Gharbhetibaa.Models.Booking;
using Gharbhetibaa.Models.Item;
using Gharbhetibaa.Models.SearchingForItem;
using Gharbhetibaa.Models.WishList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gharbhetibaa.ViewModels
{
    public class MyActivityVM
    {
        public int ItemsAdded { get; set; }
        public int SearchingAdded { get; set; }

        public int ItemsBooked { get; set; }
        public int SearchingBooked { get; set; }

        public int ItemsWishListed { get; set; }
        public int SearchingWishListed { get; set; }

    }
}