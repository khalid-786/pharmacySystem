using PharmacyManagment.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PharmacyManagment.Models.ViewModels.OrderIn
{
    public class PayementOrderInVM
    {
        public PayementOrderInVM() { }
        public PayementOrderInVM(OrderInDTO row)
        {
            this.Id = row.Id;
            this.OrderId = row.OrderId;
            this.TotalePrice = row.TotalePrice;
            this.TotaleQuantity = row.TotaleQuantity;
            this.PaidPrice = row.PaidPrice;
            this.UnpaidPrice = row.UnpaidPrice;
            this.OrderDate = row.OrderDate;
            this.PayementDate = row.PayementDate;
        }

        public long Id { get; set; }
        public long OrderId { get; set; }
        [Display(Name = "المبلغ الكلي")]
        public int TotalePrice { get; set; }
        [Display(Name = "كمية الأدوية")]
        public int TotaleQuantity { get; set; }
        [Display(Name = "المبلغ المدفوع")]
        public int PaidPrice { get; set; }
        [Display(Name = "المبلغ الغير مدفوع")]
        public int UnpaidPrice { get; set; }
        [Display(Name = "تاريخ الشراء")]
        public DateTime OrderDate { get; set; }
        [Display(Name = "تاريخ التسديد")]

        public DateTime PayementDate { get; set; }
    }
}