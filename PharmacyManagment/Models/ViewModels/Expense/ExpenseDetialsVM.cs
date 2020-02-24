using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PharmacyManagment.Models.ViewModels.Expense
{
    public class ExpenseDetialsVM
    {
        public int Id { get; set; }
        [Display(Name = "عنوان الصرفية ")]

        public string Expense_Title { get; set; }
        [Display(Name = "الموظف الذي أجراء عملية الصرف")]
        public string UserName { get; set; }
        [Display(Name = "تاريخ الصرف ")]
        public DateTime Expense_Date { get; set; }
        [Display(Name = "الكمية المطلوبة")]

        public int Expense_Quantitative { get; set; }
    }
}