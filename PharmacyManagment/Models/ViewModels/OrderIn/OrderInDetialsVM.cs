using PharmacyManagment.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PharmacyManagment.Models.ViewModels.OrderIn
{
    public class OrderInDetialsVM
    {
        public OrderInDetialsVM() { }
        public OrderInDetialsVM(OrderInDetialsDTO row)
        {
            this.Id = row.Id;
            this.OrderId = row.OrderId;
            this.MedicineId = row.MedicineId;
            this.MedicineName = row.MedicineName;
            this.MedicineQuantity = row.MedicineQuantity;
            this.MedicinePrice = row.MedicinePrice;
            this.TotalPrice = row.TotalPrice;
        }
        public long Id { get; set; }
        public long OrderId { get; set; }
        
        public int MedicineId { get; set; }
        [Display(Name = "إسم الدواء")]
        public string MedicineName { get; set; }
        [Display(Name = "الكمية المشتراه")]
        public int MedicineQuantity { get; set; }
        [Display(Name = "سعر الشراء")]
        public int MedicinePrice { get; set; }
        [Display(Name = "السعر الكلي")]
        public int TotalPrice { get; set; }
    }
}