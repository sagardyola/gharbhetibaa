using Gharbhetibaa.Models;
using Gharbhetibaa.Models.Item;
using Gharbhetibaa.Models.SearchingForItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gharbhetibaa.ViewModels
{
    public class DashboardVM
    {
        public int CountUser;
        public int CountActive;
        public int CountInactive;
        public int CountVerified;
        public int CountNonVerified;

        public int CountItem;
        public int CountSearching;

        public int CountContacted;
        public int CountNewsletter;

        public Pager Pager { get; set; }


        public IEnumerable<UserAccount> UserAccounts { get; set; }

        public IEnumerable<ContactUs> ContactUss { get; set; }
        public IEnumerable<Feedback> Feedbacks { get; set; }
        public IEnumerable<Newsletter> Newsletters { get; set; }

        public IEnumerable<ItemDetail> ItemDetails { get; set; }
        public IEnumerable<SearchingFor> SearchingFors { get; set; }

    }
}