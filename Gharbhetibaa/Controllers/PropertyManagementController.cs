using Gharbhetibaa.DAL;
using Gharbhetibaa.Models;
using Gharbhetibaa.Models.PropertyManagement;
using Gharbhetibaa.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Gharbhetibaa.Models.Images;

namespace Gharbhetibaa.Controllers
{
    [Authorize]
    public class PropertyManagementController : Controller
    {
        private GharbhetibaaDbContext db = new GharbhetibaaDbContext();
        readonly int pageSize = 10;

        // GET: PropertyManagement
        public ActionResult Index()
        {
            PropertyManagementVM model = new PropertyManagementVM();
            model.UserAccount = db.UserAccounts.Where(u => u.UserName.Equals(User.Identity.Name.ToString())).FirstOrDefault();
            return View(model);
        }

        public ActionResult Home()
        {
            UserAccount activeUserObj = db.UserAccounts.Where(u => u.UserName.Equals(User.Identity.Name.ToString())).FirstOrDefault();

            var propertyAdd = db.UserProperties.Where(x => x.UserID == activeUserObj.UserID).Select(x => x.Property).ToList();
            var tenantAdd = db.UserTenants.Where(x => x.UserID == activeUserObj.UserID).Select(x => x.Tenant).ToList();
            var supplierAdd = db.UserSuppliers.Where(x => x.UserID == activeUserObj.UserID).Select(x => x.Supplier).ToList();

            var income = db.UserRentDetails.Where(x => x.UserID == activeUserObj.UserID).Select(x => x.RentDetail).ToList();
            var expense = db.UserExpenses.Where(x => x.UserID == activeUserObj.UserID).Select(x => x.Expense).ToList();

            TrackPaymentsVM model = new TrackPaymentsVM();

            model = new TrackPaymentsVM()
            {
                PropertyAdded = propertyAdd.Count(),
                TenantAdded = tenantAdd.Count(),
                SupplierAdded = supplierAdd.Count(),
                Income = income.Sum(x => x.AmountPayed),
                Expense = expense.Sum(x => x.TotalAmount)
            };
            return PartialView(model);
        }

        public ActionResult MyProperties(int? pageNo)
        {
            var model = new PropertyManagementVM();
            UserAccount usr = db.UserAccounts.Where(u => u.UserName.Equals(User.Identity.Name.ToString())).FirstOrDefault();
            if (usr != null)
            {
                var propertyObj = db.UserProperties.Where(e => e.UserAccount.UserID == usr.UserID).Select(e => e.Property).AsEnumerable();

                int count = propertyObj.Count();
                pageNo = pageNo ?? 1;
                var skipCount = (pageNo.Value - 1) * pageSize;

                propertyObj = propertyObj.OrderByDescending(x => x.PropertyID).Skip(skipCount).Take(pageSize);

                model.Pager = new Pager(count, pageNo, pageSize);
                model.Properties = propertyObj;
            }
            return PartialView(model);
        }

        public ActionResult MyTenants(int? PropertyID, int? pageNo)
        {
            PopulatePropertyDropDownList();
            var model = new PropertyManagementVM();
            UserAccount usr = db.UserAccounts.Where(u => u.UserName.Equals(User.Identity.Name.ToString())).FirstOrDefault();

            //Dropdown
            if(PropertyID.HasValue && PropertyID.Value > 0)
            {
                //model.Properties = db.UserTenants.Where(e => e.UserAccount.UserID == usr.UserID).Select(e => e.Property).ToList();
                var tenantObj = db.UserTenants.Where(e => e.UserAccount.UserID == usr.UserID).Where(e => e.PropertyID == PropertyID).Select(e => e.Tenant).AsEnumerable();
                

                int count = tenantObj.Count();
                pageNo = pageNo ?? 1;
                var skipCount = (pageNo.Value - 1) * pageSize;

                tenantObj = tenantObj.OrderByDescending(x => x.TenantID).Skip(skipCount).Take(pageSize);

                model.Pager = new Pager(count, pageNo, pageSize);
                model.Tenants = tenantObj;
            } else
            {
                //model.Properties = db.UserTenants.Where(e => e.UserAccount.UserID == usr.UserID).Select(e => e.Property).ToList();
                var tenantObj = db.UserTenants.Where(e => e.UserAccount.UserID == usr.UserID).Select(e => e.Tenant).AsEnumerable();

                int count = tenantObj.Count();
                pageNo = pageNo ?? 1;
                var skipCount = (pageNo.Value - 1) * pageSize;

                tenantObj = tenantObj.OrderByDescending(x => x.TenantID).Skip(skipCount).Take(pageSize);

                model.Pager = new Pager(count, pageNo, pageSize);
                model.Tenants = tenantObj;
            }

            model.PropertyID = PropertyID ?? default(int); ;
            model.UserTenants = db.UserTenants;

            return PartialView(model);
        }

        public ActionResult MyExpenses(int? PropertyID, int? pageNo)
        {
            PopulatePropertyDropDownList();
            var model = new PropertyManagementVM();
            UserAccount usr = db.UserAccounts.Where(u => u.UserName.Equals(User.Identity.Name.ToString())).FirstOrDefault();

            //Dropdown
            if (PropertyID.HasValue && PropertyID.Value > 0)
            {
                var expenseObj = db.UserExpenses.Where(e => e.UserAccount.UserID == usr.UserID).Where(e => e.PropertyID == PropertyID).Select(e => e.Expense).AsEnumerable();
                


                int count = expenseObj.Count();
                pageNo = pageNo ?? 1;
                var skipCount = (pageNo.Value - 1) * pageSize;

                expenseObj = expenseObj.OrderByDescending(x => x.ExpenseID).Skip(skipCount).Take(pageSize);

                model.Pager = new Pager(count, pageNo, pageSize);
                model.Expenses = expenseObj;
            } else
            {

                var expenseObj = db.UserExpenses.Where(e => e.UserAccount.UserID == usr.UserID).Select(e => e.Expense).AsEnumerable();
                int count = expenseObj.Count();
                pageNo = pageNo ?? 1;
                var skipCount = (pageNo.Value - 1) * pageSize;

                expenseObj = expenseObj.OrderByDescending(x => x.ExpenseID).Skip(skipCount).Take(pageSize);

                model.Pager = new Pager(count, pageNo, pageSize);
                model.Expenses = expenseObj;
            }

            model.PropertyID = PropertyID ?? default(int); ;
            model.UserExpenses = db.UserExpenses;
            return PartialView(model);
        }

        public ActionResult MySuppliers(int? pageNo)
        {
            var model = new PropertyManagementVM();
            UserAccount usr = db.UserAccounts.Where(u => u.UserName.Equals(User.Identity.Name.ToString())).FirstOrDefault();

            if (usr != null)
            {
                var supplierObj = db.UserSuppliers.Where(x => x.UserID == usr.UserID).Select(x => x.Supplier).AsEnumerable();

                int count = supplierObj.Count();
                pageNo = pageNo ?? 1;
                var skipCount = (pageNo.Value - 1) * pageSize;

                supplierObj = supplierObj.OrderByDescending(x => x.SupplierID).Skip(skipCount).Take(pageSize);

                model.Pager = new Pager(count, pageNo, pageSize);
                model.Supplier = supplierObj;
            }


            return PartialView(model);
        }

        private void PopulatePropertyDropDownList(object selectedItemFor = null)
        {
            UserAccount usr = db.UserAccounts.Where(u => u.UserName.Equals(User.Identity.Name.ToString())).FirstOrDefault();
            var propertyQuery = db.UserProperties.Where(x => x.UserID == usr.UserID).Select(x => x.Property);
            ViewBag.PropertyID = new SelectList(propertyQuery, "PropertyID", "Title", selectedItemFor);
        }
    }
}