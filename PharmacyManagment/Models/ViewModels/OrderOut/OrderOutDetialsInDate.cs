using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PharmacyManagment.Models.ViewModels.OrderOut
{
    public class OrderOutDetialsInDate
    {
        public OrderOutDetialsInDate() { }
        public int Id { get; set; }
        [Display(Name = "إسم الدواء ")]
         
        public string MedincineName { get; set; }
     
        public int MedicineId { get; set; }
        [Display(Name = "تاريخ البيع ")]
        public DateTime OrderDate { get; set; }
        [Display(Name = "كمية الدواء")]

        public decimal MedicineQuantity { get; set; }
        [Display(Name = "سعر الدواء")]
        public int MedicinePrice { get; set; }
        [Display(Name = "السعر الكلي")]
        public decimal OrderTotale { get; set; }
        public int MedicineDetialsId { get; set; }
        [Display(Name = "هل يحتوي علي أشرطة")]
        public bool MedicineIsTapes { get; set; }
    }
}