using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NBSP.Models
{
    public class Donation
    {
        [Display(Name = "Donation ID")]
        public int DonationID { get; set; }

        [Display(Name = "Donation Date")]
        [DataType(DataType.DateTime)]
        public DateTime DonationDate { get; set; }

        [Display(Name = "Donation Name")]
        [StringLength(50)]
        public string? Name { get; set; }

        [Display(Name = "Amount")]
        public int Money { get; set; }

        [Display(Name = "Description")]
        [StringLength(255)]
        public string? Description { get; set; }

    }
}
