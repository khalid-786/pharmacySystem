using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace JobsOffice.Models
{
    public class ApplyForJob
    {
        public int Id { get; set; }
        [Display(Name = "رسالة الباحث عن الوظيفة")]
        public string Message { get; set; }
        [Display(Name = "تاريخ التقدم للوظيفة")]
        public DateTime ApplyDate { get; set; }
        public int JobId { get; set; }
        public string UserId { get; set; }
        public virtual Job job { get; set; }
        public virtual ApplicationUser user { get; set; }

    }
}