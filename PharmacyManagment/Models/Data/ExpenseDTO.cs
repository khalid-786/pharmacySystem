using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PharmacyManagment.Models.Data
{
    [Table("Expensetbl")]
    public class ExpenseDTO
    {

        [Key]
        public int Id { get; set; }
        public string Expense_Title { get; set; }
        public int UserId { get; set; }
        public DateTime Expense_Date { get; set; }

        public DateTime Expense_Day { get; set; }
        public int Expense_Quantitative { get; set; }
        [ForeignKey("UserId")]
        public virtual EmployeeDTO User { get; set; }
    }
}