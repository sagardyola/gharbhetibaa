using Gharbhetibaa.Models;
using Gharbhetibaa.Models.Booking;
using Gharbhetibaa.Models.Images;
using Gharbhetibaa.Models.Item;
using Gharbhetibaa.Models.PostComment;
using Gharbhetibaa.Models.SearchingForItem;
using Gharbhetibaa.Models.WishList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gharbhetibaa.ViewModels
{
    public class ItemData
    {
        public int CountItem { get; set; }

        public Pager Pager { get; set; }

        public virtual UserAccount UserAccount { get; set; }

        public IEnumerable<ItemDetail> ItemDetails { get; set; }

        public IEnumerable<UserItemDetail> UserItemDetails { get; set; }
        public IEnumerable<WishListItem> WishListItems { get; set; }
        public IEnumerable<BookingItem> BookingItems { get; set; }

        public IEnumerable<CommentItem> CommentItems { get; set; }
        public virtual CommentItem CommentItem { get; set; }
    }
}