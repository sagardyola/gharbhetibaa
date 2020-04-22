using Gharbhetibaa.Models;
using Gharbhetibaa.Models.Booking;
using Gharbhetibaa.Models.Images;
using Gharbhetibaa.Models.Item;
using Gharbhetibaa.Models.PostComment;
using Gharbhetibaa.Models.PropertyManagement;
using Gharbhetibaa.Models.SearchingForItem;
using Gharbhetibaa.Models.WishList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Gharbhetibaa.DAL
{
    public class GharbhetibaaDbContext : DbContext
    {
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }


        public DbSet<ContactUs> ContactUss { get; set; }
        public DbSet<Newsletter> Newsletters { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

        public DbSet<Property> Properties { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<RentDetail> RentDetails { get; set; }
        public DbSet<UserProperty> UserProperties { get; set; }
        public DbSet<UserTenant> UserTenants { get; set; }
        public DbSet<UserRentDetail> UserRentDetails { get; set; }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<UserExpense> UserExpenses { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<UserSupplier> UserSuppliers { get; set; }

        public DbSet<ItemDetail> ItemDetails { get; set; }
        public DbSet<ItemFor> ItemFors { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }
        public DbSet<LookingFor> LookingFors { get; set; }
        public DbSet<Lifestyle> Lifestyles { get; set; }
        public DbSet<RoomDetail> RoomDetails { get; set; }
        public DbSet<OtherFeature> OtherFeatures { get; set; }

        public DbSet<UserItemDetail> UserItemDetails { get; set; }
        public DbSet<UserFeature> UserFeatures { get; set; }


        public DbSet<WishListItem> WishListItems { get; set; }
        public DbSet<WishListSearching> WishListSearchings { get; set; }

        public DbSet<BookingItem> BookingItems { get; set; }
        public DbSet<BookingSearching> BookingSearchings { get; set; }

        public DbSet<CommentItem> CommentItems { get; set; }
        public DbSet<CommentSearching> CommentSearchings { get; set; }

        public DbSet<SearchingFor> SearchingFors { get; set; }
        public DbSet<UserSearchingFor> UserSearchingFors { get; set; }

        public DbSet<Picture> Pictures { get; set; }
        public DbSet<PictureItem> PictureItems { get; set; }
        public DbSet<PictureContractDoc> PictureContractDocs { get; set; }







        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<UserAccount>().Ignore(m => m.ConfirmPassword);
            //base.OnModelCreating(modelBuilder);







            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //modelBuilder
            //    .Entity<UserAccount>()
            //    .HasMany(c => c.Properties)
            //    .WithMany(i => i.UserAccounts)
            //    .Map(t => t.MapLeftKey("UserID").MapRightKey("PropertyID").ToTable("UserAccountProperty"));
            //                .MapToStoredProcedures(s=> s.Insert(a => a.HasName("add_property").LeftKeyParameter(p => p.UserID, "user_id").RightKeyParameter(t => t.PropertyID, "property_id")));

            //modelBuilder.Entity<UserProperty>()
            //.HasRequired(b => b.Property)
            //.WithMany()
            //.WillCascadeOnDelete(true);



            //base.OnModelCreating(modelBuilder);
        }


    }
}