using Gharbhetibaa.Models;
using Gharbhetibaa.Models.Booking;
using Gharbhetibaa.Models.PostComment;
using Gharbhetibaa.Models.SearchingForItem;
using Gharbhetibaa.Models.WishList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gharbhetibaa.ViewModels
{
    public class SearchingForVM
    {
        public int CountSearchingFor { get; set; }
        public Pager Pager { get; set; }

        public virtual UserAccount UserAccount { get; set; }

        public IEnumerable<SearchingFor> SearchingFors { get; set; }
        public IEnumerable<UserSearchingFor> UserSearchingFors { get; set; }

        public IEnumerable<WishListSearching> WishListSearchings { get; set; }
        public IEnumerable<BookingSearching> BookingSearchings { get; set; }

        public IEnumerable<CommentSearching> CommentSearchings { get; set; }
        public virtual CommentSearching CommentSearching { get; set; }
    }
}