using Gharbhetibaa.Models.Item;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Gharbhetibaa.Models.Images
{
    public class PictureItem
    {
        [Key]
        [Column(Order = 1)]
        public int ItemID { get; set; }

        [Key]
        [Column(Order = 2)]
        public int ImageID { get; set; }

        public virtual ItemDetail ItemDetail { get; set; }
        public virtual Picture Picture { get; set; }
    }
}