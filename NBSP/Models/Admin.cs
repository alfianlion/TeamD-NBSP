using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NBSP.Models
{
    public class Admin
    {
        [Display(Name = "Admin ID")]
        public int AdminID { get; set; }

        [Display(Name = "Password")]
        [StringLength(255)]
        public string Pwd { get; set; }

    }
}
