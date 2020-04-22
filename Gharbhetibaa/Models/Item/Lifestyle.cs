using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Gharbhetibaa.Models.Item
{
    public class Lifestyle
    {
        [Key]
        public int LifestyleID { get; set; }

        [Display(Name = "Overnight Guests")]
        public string OvernightGuests { get; set; }

        [Display(Name = "Party Habits")]
        public string PartyHabits { get; set; }

        [Display(Name = "Smoker")]
        public string Smoker { get; set; }

        [Display(Name = "Pets Friendly")]
        public string PetsFriendly { get; set; }
    }
}