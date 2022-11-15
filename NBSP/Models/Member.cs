using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NBSP.Models
{
    public class Member
    {
        [Display(Name = "Member ID")] 
        public int MemberID { get; set; }
        [Display(Name = "Name")] [StringLength(50)]
        public string Name { get; set; }

        [Display(Name = "Email Address")] 
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail adress")] 
        [StringLength(255)] public string EmailAddr { get; set; }
        [Display(Name = "Phone Number")] 
        public int PhoneNo { get; set; }

        [Display(Name = "Password")] 
        [StringLength(255)] 
        public string Pwd { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare("Pwd", ErrorMessage = "Please ensure both password are the same")]
        public string PasswordConfirm { get; set; }

    }
}
