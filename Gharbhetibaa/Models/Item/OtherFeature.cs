using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Gharbhetibaa.Models.Item
{
    public class OtherFeature
    {
        [Key]
        public int OtherFeatureID { get; set; }

        [Display(Name = "Garden")]
        public bool Garden { get; set; }

        [Display(Name = "Parking Space")]
        public bool ParkingSpace { get; set; }

        [Display(Name = "Garage")]
        public bool Garage { get; set; }

        [Display(Name = "Security Guard")]
        public bool SecurityGuard { get; set; }

        [Display(Name = "Backup Generator")]
        public bool BackupGenerator { get; set; }

        [Display(Name = "Water Supply")]
        public bool WaterSupply { get; set; }

        [Display(Name = "Internet")]
        public bool Internet { get; set; }

        [Display(Name = "Gymnasium")]
        public bool Gymnasium { get; set; }

        [Display(Name = "Swimming Pool")]
        public bool SwimmingPool { get; set; }

        [Display(Name = "Elevator")]
        public bool Elevator { get; set; }
    }
}