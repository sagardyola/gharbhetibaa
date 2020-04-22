using Gharbhetibaa.Models.Item;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Gharbhetibaa.Models.PostComment
{
    public class CommentItem
    {
        private DateTime _date = DateTime.Now;

        [Key]
        public int CommentID { get; set; }

        [Display(Name = "Write a comment...")]
        [Required(ErrorMessage = "Please enter your comment")]
        public string CommentTitle { get; set; }

        [Display(Name = "Commented On")]
        public DateTime CommentedOn
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

        public int UserID { get; set; }
        public int ItemID { get; set; }

        [ForeignKey("UserID")]
        public virtual UserAccount UserAccount { get; set; }

        [ForeignKey("ItemID")]
        public virtual ItemDetail ItemDetail { get; set; }
    }
}