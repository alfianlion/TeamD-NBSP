using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NBSP.Models
{
    public class Volunteer
    {
        [Display(Name = "Volunteer ID")] 
        public int VolunteerID { get; set; }

        [Display(Name = "Volunteer Name")] 
        [StringLength(50)] 
        public string Name { get; set; }

        [Display(Name = "Email Address")]
        [StringLength(255)] 
        public string EmailAddr { get; set; }

        [Display(Name = "Contact Number")] 
        public int ContactNo { get; set; }

        [Display(Name = "Password")]
        [StringLength(255)] 
        public string Pwd { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare("Pwd", ErrorMessage = "Please ensure both password are the same")]
        public string PasswordConfirm { get; set; }

        [Display(Name = "Date Of Birth")] 
        [DataType(DataType.DateTime)] 
        public DateTime? DOB { get; set; }

        [Display(Name = "Gender")] 
        [StringLength(1)] 
        public string? Gender { get; set; }

        [Display(Name = "Monday Avaliability")] 
        [RegularExpression(@"[0-1]")] [StringLength(1)]
        public bool? Mon { get; set; }

        [Display(Name = "Tuesday Avaliability")]
        [RegularExpression(@"[0-1]")] [StringLength(1)]
        public bool? Tue { get; set; }

        [Display(Name = "Wednesday Avaliability")] 
        [RegularExpression(@"[0-1]")] [StringLength(1)] 
        public bool? Wed { get; set; }

        [Display(Name = "Thursday Avaliability")]
        [RegularExpression(@"[0-1]")] [StringLength(1)] 
        public bool? Thur { get; set; }

        [Display(Name = "Friday Avaliability")]
        [RegularExpression(@"[0-1]")] [StringLength(1)] 
        public bool? Fri { get; set; }

        [Display(Name = "Saturday Avaliability")]
        [RegularExpression(@"[0-1]")] [StringLength(1)] 
        public bool? Sat { get; set; }

        [Display(Name = "Sunday Avaliability")] 
        [RegularExpression(@"[0-1]")] [StringLength(1)] 
        public bool? Sun { get; set; }
    }
}
