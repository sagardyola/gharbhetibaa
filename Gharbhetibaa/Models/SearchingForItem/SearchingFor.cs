using Gharbhetibaa.Models.Item;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Gharbhetibaa.Models.SearchingForItem
{
    public class SearchingFor
    {
        private DateTime _date = DateTime.Now;
        string count;

        [Key]
        public int SearchingForID { get; set; }

        [Display(Name = "Searching Wish Code")]
        public string SearchingCode { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Minimum Price")]
        public int? MinPrice { get; set; }

        [Display(Name = "Maximum Price")]
        public int? MaxPrice { get; set; }

        [Display(Name = "Price Range")]
        public string PriceRange
        {
            get
            {
                return MinPrice + " to " + MaxPrice;
            }
        }

        [Display(Name = "Run Ad For")]
        public string AdRunDays
        {
            get
            {
                return count;
            }
            set
            {
                count = Regex.Match(value, @"\d+").Value; ;
            }
        }

        [Display(Name = "Expires On")]
        public DateTime AdExpiryDate
        {
            get
            {
                if (PostedOn != null && AdRunDays != null)
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
        [Required(ErrorMessage = "Please Enter the location")]
        public string Location { get; set; }

        [Display(Name = "City")]
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

        public virtual ICollection<UserSearchingFor> UserSearchingFors { get; set; }
    }
}