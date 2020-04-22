using Gharbhetibaa.Models.Images;
using Gharbhetibaa.Models.Item;
using Gharbhetibaa.Models.PropertyManagement;
using Gharbhetibaa.Models.SearchingForItem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Gharbhetibaa.Models
{
    public class UserAccount
    {
        private DateTime _date = DateTime.Now;

        [Key]
        public int UserID { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "Please enter a username.")]
        public string UserName { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "You need to enter your first name.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "You need to enter your last name.")]
        public string LastName { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "You need to enter your address.")]
        public string Address { get; set; }

        [Display(Name = "Mobile")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string MobileNumber { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "You need to enter your email address.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "About Me")]
        public string AboutMe { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "You must have your password.")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        public bool Status { get; set; }

        [DisplayName("Image")]
        public string ImageURL { get; set; }

        public bool IsVerified { get; set; }

        [Display(Name = "Activation Code")]
        public string ActivationCode { get; set; }

        [Display(Name = "Reset Code")]
        public string ResetCode { get; set; }

        [Display(Name = "Created On")]
        public DateTime CreatedOn
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

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public virtual ICollection<UserSupplier> UserSuppliers { get; set; }

        public virtual ICollection<UserExpense> UserExpenses { get; set; }


        public virtual ICollection<UserProperty> UserProperties { get; set; }
        public virtual ICollection<UserTenant> UserTenants { get; set; }
        public virtual ICollection<UserRentDetail> UserRentDetails { get; set; }

        public virtual ICollection<UserItemDetail> UserItemDetails { get; set; }
        public virtual ICollection<UserFeature> UserFeatures { get; set; }

        public virtual ICollection<UserSearchingFor> UserSearchingFors { get; set; }

        
        public virtual List<UserRole> UserRoles { get; set; }
    }

    public enum Gender
    {
        Male,
        Female,
        Other
    }
}