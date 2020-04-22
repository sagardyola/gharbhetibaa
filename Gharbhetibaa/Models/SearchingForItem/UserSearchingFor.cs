using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Gharbhetibaa.Models.SearchingForItem
{
    public class UserSearchingFor
    {
        [Key]
        [Column(Order = 1)]
        public int UserID { get; set; }

        [Key]
        [Column(Order = 2)]
        public int SearchingForID { get; set; }

        public virtual UserAccount UserAccount { get; set; }
        public virtual SearchingFor SearchingFor { get; set; }
    }
}