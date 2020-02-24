using PharmacyManagment.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PharmacyManagment.Models.ViewModels.Expense
{
    public class ExpenseVM
    {
        public ExpenseVM() { }

        public ExpenseVM(ExpenseDTO row) {
            this.Id = row.Id;
            this.UserId = row.UserId;
            this.Expense_Title = row.Expense_Title;
            this.Expense_Quantitative = row.Expense_Quantitative;
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "  رجاءاإدخل عنوان الصرفية ")]
        [Display(Name = "عنوان الصرفية ")]

        public string Expense_Title { get; set; }
        public int UserId { get; set; }
        [Display(Name = "تاريخ الصرف ")]
        public DateTime Expense_Date { get; set; }
        [Required(ErrorMessage = "  رجاءاإدخل الكمية المطلوبة المراد صرفها ")]
        [Display(Name = "الكمية المطلوبة")]

        public int Expense_Quantitative { get; set; }
    }
}