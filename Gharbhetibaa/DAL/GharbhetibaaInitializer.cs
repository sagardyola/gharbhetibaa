using Gharbhetibaa.Models;
using Gharbhetibaa.Models.Item;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace Gharbhetibaa.DAL
{
    public class GharbhetibaaInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<GharbhetibaaDbContext>
    {
        protected override void Seed(GharbhetibaaDbContext context)
        {
            var usrObj = new List<Role>
            {
                new Role{ Name = "Admin"},
                new Role{ Name = "User"}
            };
            usrObj.ForEach(s => context.Roles.Add(s));
            context.SaveChanges();


            var iFor = new List<ItemFor>
            {
                new ItemFor{ItemForTitle = "Rent"},
                new ItemFor{ItemForTitle = "Sale"},
                new ItemFor{ItemForTitle = "Homestay"},
                new ItemFor{ItemForTitle = "Roommate"}
            };
            iFor.ForEach(s => context.ItemFors.Add(s));
            context.SaveChanges();

            var iType = new List<ItemType>
            {
                new ItemType{ItemTypeTitle = "Room"},
                new ItemType{ItemTypeTitle = "Flat"},
                new ItemType{ItemTypeTitle = "Apartment"},
                new ItemType{ItemTypeTitle = "House"},
                new ItemType{ItemTypeTitle = "Land"},
                new ItemType{ItemTypeTitle = "Office Space"},
                new ItemType{ItemTypeTitle = "Shop Space"},
                new ItemType{ItemTypeTitle = "Commercial Property"}
            };
            iType.ForEach(s => context.ItemTypes.Add(s));
            context.SaveChanges();


            var hashedPassword = Crypto.HashPassword("111111");
            context.UserAccounts.AddOrUpdate(u => u.UserName,
                new UserAccount {
                    UserName = "sagardyola",
                    FirstName = "Sagar",
                    LastName = "Dyola",
                    Address = "Lagankhel",
                    Email = "sagardyola@gmail.com",
                    Password = hashedPassword,
                    Status = true,
                    IsVerified = true,
                    ResetCode = null
                });

            context.UserRoles.AddOrUpdate(u => u.UserID,
                new UserRole
                {
                    UserID = 1,
                    RoleID = 1
                });

            base.Seed(context);
        }
    }
}