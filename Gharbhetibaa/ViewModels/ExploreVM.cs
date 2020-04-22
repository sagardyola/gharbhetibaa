using Gharbhetibaa.Models.Images;
using Gharbhetibaa.Models.Item;
using Gharbhetibaa.Models.SearchingForItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gharbhetibaa.ViewModels
{
    public class ExploreVM
    {
        public string SearchType;
        public string SearchText;
        public int Count;

        public int ItemForID;
        public int ItemTypeID;

        public Pager Pager { get; set; }

        public virtual ItemDetail ItemDetail { get; set; }
        public IEnumerable<ItemDetail> ItemDetails { get; set; }
        public IEnumerable<UserItemDetail> UserItemDetails { get; set; }

        public virtual SearchingFor SearchingFor { get; set; }
        public IEnumerable<SearchingFor> SearchingFors { get; set; }
        public IEnumerable<UserSearchingFor> UserSearchingFors { get; set; }

        public IEnumerable<ItemFor> ItemFors { get; set; }
        public IEnumerable<ItemType> ItemTypes { get; set; }


        public virtual Picture Picture { get; set; }
        public virtual PictureItem PictureItem { get; set; }
        
    }

}