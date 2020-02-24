using PharmacyManagment.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PharmacyManagment.Models.ViewModels.Account
{
    public class EmployeeVM
    {
       public EmployeeVM() {}
        public EmployeeVM(EmployeeDTO row) {
            this.Id        = row.Id;
            this.FirstName = row.FirstName;
            this.LastName  = row.LastName;
            this.Username  = row.Username;
            this.password  = row.password;
            this.PhoneNumber = row.PhoneNumber;
            
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "  رجاءاإدخل الأسم الأول")]
        [Display(Name = "الأسم الأول")]
        
        public string FirstName { get; set; }
        [Required(ErrorMessage = "رجاءاإدخل سم العائلة")]
        [Display(Name = " إسم العائلة ")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "رجاءاإدخل إسم المستخدم")]
        [Display(Name = "إسم المستخدم")]
        public string Username { get; set; }
        [Required(ErrorMessage = "رجاءاإدخل كلمة المرور")]
        [Display(Name = "كلمة المرور")]
        public string password { get; set; }
        [Required(ErrorMessage = "رجاءاإعد كتابة كلمة المرور")]
        [Display(Name = "إعادة كلمة المرور")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = " رجاءاإدخل رقم التلفون")]
        [Display(Name = "رقم التلفون")]
        public string PhoneNumber { get; set; }
       
    }
}