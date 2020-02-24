using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PharmacyManagment.Models.ViewModels.Account
{
    public class LoginUserVM
    {
        [Required(ErrorMessage = "رجاءاإدخل إسم المستخدم")]
        [Display(Name = "إسم المستخدم")]
        public string Username { get; set; }
        [Required(ErrorMessage = "رجاءاإدخل كلمة المرور")]
        [Display(Name = "كلمة المرور")]
        public string password { get; set; }
        [Display(Name = "تذكرني")]
        public bool RememberMe { get; set; }
    }
}