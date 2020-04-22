using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Gharbhetibaa.Models.PropertyManagement
{
    public class UserTenant
    {
        [Key]
        [Column(Order = 1)]
        public int UserID { get; set; }

        [Key]
        [Column(Order = 2)]
        public int PropertyID { get; set; }

        [Key]
        [Column(Order = 3)]
        public int TenantID { get; set; }

        public virtual UserAccount UserAccount { get; set; }
        public virtual Property Property { get; set; }
        public virtual Tenant Tenant { get; set; }
    }
}