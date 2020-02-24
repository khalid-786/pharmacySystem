using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PharmacyManagment.Models.ViewModels.OrderOut
{
    public class CurentOrderDetialsVM
    {
        [Display(Name = "إسم الدواء ")]
        public string MedincineName { get; set; }
        [Display(Name = "كمية الدواء")]
        public decimal MedicineQuantity { get; set; }
        [Display(Name = "سعر الدواء")]
        public int MedicinePrice { get; set; }
        [Display(Name = "السعر الكلي")]
        public decimal OrderTotale { get; set; }
        [Display(Name = "هل يحتوي علي أشرطة")]
        public bool MedicineIsTapes { get; set; }
    }
}