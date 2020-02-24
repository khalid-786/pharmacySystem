using PharmacyManagment.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PharmacyManagment.Models.ViewModels.OrderOut
{
    public class OrderOutDetialsVM
    {
        public OrderOutDetialsVM() { }
        public OrderOutDetialsVM(OrderOutDetialsDTO row) {
            this.Id = row.Id;
            this.OrderId = row.OrderId;
            this.MedicineName = row.MedicineName;
            this.MedicineId = row.MedicineId;
            this.MedicinePrice = row.MedicinePrice;
            this.OrderDate = row.OrderDate;
            this.OrderDay = row.OrderDay;
            this.TotaleAmount = row.TotaleAmount;
            this.QTY = row.QTY;
        }
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int MedicineId { get; set; }
        [Display(Name = "إسم الدواء ")]
        public string MedicineName { get; set; }
        [Display(Name = "سعر الدواء")]
        public int MedicinePrice { get; set; }
        [Display(Name = "تاريخ البيع ")]
        public DateTime OrderDate { get; set; }
        [Display(Name = " اليوم  ")]
        public DateTime OrderDay { get; set; }
        [Display(Name = "السعر الكلي")]
        public int TotaleAmount { get; set; }
        [Display(Name = "كمية الدواء")]
        public int QTY { get; set; }
    }
}