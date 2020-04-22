using Gharbhetibaa.Models;
using Gharbhetibaa.Models.PropertyManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gharbhetibaa.ViewModels
{
    public class PropertyManagementVM
    {
        public int CountProperty { get; set; }
        public int CountTenant { get; set; }
        public int CountSupplier { get; set; }
        public int CountExpense { get; set; }

        public string FilterProperty { get; set; }

        public int PropertyID;
        public int TenantID;

        public string PropertyName;
        public string TenantName;
        public Pager Pager { get; set; }

        public virtual UserAccount UserAccount { get; set; }

        public IEnumerable<UserAccount> UserAccounts { get; set; }

        //public virtual Property Property { get; set; }
        //public virtual Tenant Tenant { get; set; }
        //public virtual RentDetail RentDetail { get; set; }


        public IEnumerable<Property> Properties { get; set; }
        public IEnumerable<Tenant> Tenants { get; set; }
        public IEnumerable<Property> UserProperties { get; set; }
        public IEnumerable<RentDetail> RentDetails { get; set; }
        public IEnumerable<UserTenant> UserTenants { get; set; }
        public IEnumerable<UserRentDetail> UserRentDetails { get; set; }

        public IEnumerable<Expense> Expenses { get; set; }
        public IEnumerable<UserExpense> UserExpenses { get; set; }

        public IEnumerable<Supplier> Supplier { get; set; }
        public IEnumerable<UserSupplier> UserSuppliers { get; set; }

    }
}