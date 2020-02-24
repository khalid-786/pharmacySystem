using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace JobsOffice.Models
{
    public class Role
    {
        public string Id { get; set; }
        [Display(Name = "إسم المجموعة")]
        public string Name { get; set; }
        
    }
}