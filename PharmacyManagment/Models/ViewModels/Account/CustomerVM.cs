using PharmacyManagment.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PharmacyManagment.Models.ViewModels.Account
{
    public class CustomerVM
    {
        public CustomerVM() { }
        public CustomerVM(CustomerDTO row)
        {
            this.Id = row.Id;
            this.CustomerName = row.CustomerName;
            this.CompanyName = row.CompanyName;
            this.PhoneNumber = row.PhoneNumber;
            
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "  رجاءاإدخل إسم العميل ")]
        [Display(Name = "إسم العميل")]

        public string CustomerName { get; set; }
        [Required(ErrorMessage = "رجاءاإدخل سم إسم الشركة التابع لها العميل")]
        [Display(Name = " إسم الشركة التابع لها العميل ")]
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "  رجاءاإدخل رقم التلفون العميل")]
        [Display(Name = "رقم التلفون")]
        public string PhoneNumber { get; set; }
    }
}