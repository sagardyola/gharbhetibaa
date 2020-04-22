using Gharbhetibaa.Models.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gharbhetibaa.ViewModels
{
    public class HomeVM
    {
        public int CountItems { get; set; }

        public IEnumerable<ItemDetail> Showcase { get; set; }
        public IEnumerable<ItemDetail> RecentAds { get; set; }
        public IEnumerable<ItemDetail> RentSale { get; set; }
        public IEnumerable<ItemDetail> HomestayRoommate { get; set; }

    }

}