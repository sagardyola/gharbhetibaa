using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Gharbhetibaa.Models.Item
{
    public class UserFeature
    {
        [Key]
        [Column(Order = 1)]
        public int UserID { get; set; }

        [Key]
        [Column(Order = 2)]
        public int ItemID { get; set; }

        public virtual UserAccount UserAccount { get; set; }
        public virtual ItemDetail ItemDetail { get; set; }
    }
}