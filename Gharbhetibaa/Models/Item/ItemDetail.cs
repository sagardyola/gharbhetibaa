using Gharbhetibaa.Models.Images;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Gharbhetibaa.Models.Item
{
    public class ItemDetail
    {
        private DateTime _date = DateTime.Now;
        string count;

        [Key]
        public int ItemID { get; set; }

        //[Index(IsUnique = true)]
        [Display(Name = "Property Code")]
        public string ItemCode { get; set; }

        [Display(Name = "Property Title")]
        public string ItemTitle
        {
            get
            {
                return ItemType.ItemTypeTitle + " for " + ItemFor.ItemForTitle;
            }
        }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Please enter the price")]
        public Double Price { get; set; }

        [Display(Name = "Negotiable")]
        public bool PriceNegotiable { get; set; }

        [Display(Name = "Area (sq.ft)")]
        public Single? Area { get; set; }

        [Display(Name = "Run Ad For")]
        public string AdRunDays {
            get {
                return count;
            }
            set {
                count = Regex.Match(value, @"\d+").Value; ;
            }
        }

        [Display(Name = "Expires In")]
        public DateTime AdExpiryDate {
            get {
                if(PostedOn != null && AdRunDays != null)
                {
                    return PostedOn.AddDays(Int32.Parse(AdRunDays));
                }
                return _date;
            }
        }

        public bool Status
        {
            get
            {
                return AdExpiryDate.Date > DateTime.Now.Date;
            }
        }

        [Display(Name = "Location")]
        [Required(ErrorMessage = "Please Enter your location")]
        public string Location { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "Please Enter your city")]
        public string City { get; set; }

        [Display(Name = "Location")]
        public string LocationCity
        {
            get
            {
                return Location + ", " + City;
            }
        }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Posted On")]
        public DateTime PostedOn
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
            }
        }

        public int ItemForID { get; set; }
        public int ItemTypeID { get; set; }

        public int LookingForID { get; set; }
        public int LifestyleID { get; set; }
        public int RoomDetailID { get; set; }
        public int OtherFeatureID { get; set; }

        [ForeignKey("ItemForID")]
        public virtual ItemFor ItemFor { get; set; }

        [ForeignKey("ItemTypeID")]
        public virtual ItemType ItemType { get; set; }

        [ForeignKey("LookingForID")]
        public virtual LookingFor LookingFor { get; set; }

        [ForeignKey("LifestyleID")]
        public virtual Lifestyle Lifestyle { get; set; }

        [ForeignKey("RoomDetailID")]
        public virtual RoomDetail RoomDetail { get; set; }

        [ForeignKey("OtherFeatureID")]
        public virtual OtherFeature OtherFeature { get; set; }

        public virtual ICollection<UserItemDetail> UserItemDetails { get; set; }
        public virtual ICollection<UserFeature> UserFeatures { get; set; }

        public virtual List<PictureItem> PictureItems { get; set; }
    }
}