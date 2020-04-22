using Gharbhetibaa.Models.PropertyManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Gharbhetibaa.Models.Images
{
    public class PictureContractDoc
    {
        [Key]
        [Column(Order = 1)]
        public int TenantID { get; set; }

        [Key]
        [Column(Order = 2)]
        public int ImageID { get; set; }

        public virtual Tenant Tenant { get; set; }
        public virtual Picture Picture { get; set; }
    }
}