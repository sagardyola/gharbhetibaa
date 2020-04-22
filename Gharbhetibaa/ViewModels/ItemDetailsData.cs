using Gharbhetibaa.Models.Images;
using Gharbhetibaa.Models.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gharbhetibaa.ViewModels
{
    public class ItemDetailsData
    {
        public virtual ItemDetail ItemDetail { get; set; }
        public virtual Picture Picture { get; set; }
        public virtual PictureItem PictureItem { get; set; }
    }
}