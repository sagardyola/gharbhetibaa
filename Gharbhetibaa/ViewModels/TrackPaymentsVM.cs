using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gharbhetibaa.ViewModels
{
    public class TrackPaymentsVM
    {
        public int PropertyAdded { get; set; }
        public int TenantAdded { get; set; }
        public int SupplierAdded { get; set; }

        public double? Income { get; set; }
        public double? Expense { get; set; }
    }
}