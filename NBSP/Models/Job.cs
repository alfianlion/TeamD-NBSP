using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NBSP.Models
{
    public class Job
    {
        [Display(Name = "Job ID")]
        public int JobID { get; set; }

        [Display(Name = "Name")]
        public string JobName { get; set; }

        public string Summary { get; set; }
        public string Description { get; set; }
        public string Company { get; set; }
        public Decimal Salary { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNo { get; set; }

        [Display(Name = "Email Address")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail adress")]
        [StringLength(255)] public string EmailAddr { get; set; }
    }
}
