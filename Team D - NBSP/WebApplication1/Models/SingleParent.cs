using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class SingleParent
    {
        [StringLength(9, MinimumLength = 9, ErrorMessage = "ID length must be 9")]
        [Display(Name = "ID")]
        [Required]
        public string MemberID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name length cannot exceed 100 characters")]
        public string Name { get; set; }

        [Display(Name = "Telephone Number")]
        [StringLength(20, ErrorMessage = "Telephone number cannot exceed 20 characters")]
        public string? TelNo { get; set; }

        [Display(Name = "Email")]
        [StringLength(50, ErrorMessage = "Email length cannot exceed 50 characters")]
        public string? EmailAddr { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string Avaliability { get; set; }
    }
}
