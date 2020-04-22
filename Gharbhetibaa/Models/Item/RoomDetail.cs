using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Gharbhetibaa.Models.Item
{
    public class RoomDetail
    {
        [Key]
        public int RoomDetailID { get; set; }

        [Display(Name = "Living Room")]
        public bool LivingRoom { get; set; }

        [Display(Name = "Bed Room")]
        public bool BedRoom { get; set; }

        [Display(Name = "Kitchen")]
        public bool Kitchen { get; set; }

        [Display(Name = "Bathroom")]
        public bool Bathroom { get; set; }

        [Display(Name = "Balcony")]
        public bool Balcony { get; set; }
    }
}