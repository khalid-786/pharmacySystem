using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace JobsOffice.Models
{
    public class Job
    {

        public int Id { get; set; }
        [Display(Name = "إسم الوظيفة")]
        public string JobTitle { get; set; }
        [Display(Name = "وصف الوظيفة")]
        public string JobContent { get; set; }
        [Display(Name = "صورة الوظيفة")]
        public string JobImage { get; set; }
        [Display(Name = "نوع الوظيفة")]
        public int CategoryId { get; set; }
        public string UserId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}