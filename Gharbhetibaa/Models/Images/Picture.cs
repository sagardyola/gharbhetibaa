using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gharbhetibaa.Models.Images
{
    public class Picture
    {
        [Key]
        public int ImageID { get; set; }

        [Display(Name = "Image Path")]
        public string ImageLocation { get; set; }

        public virtual ICollection<PictureItem> PictureItems { get; set; }
        public virtual ICollection<PictureContractDoc> PictureContractDocs { get; set; }
    }
}