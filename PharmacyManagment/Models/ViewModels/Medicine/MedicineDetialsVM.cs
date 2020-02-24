using PharmacyManagment.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PharmacyManagment.Models.ViewModels.Medicine
{
    public class MedicineDetialsVM
    {
        public MedicineDetialsVM() { }

        public MedicineDetialsVM(MedicineDTO row)
        {
            this.Id = row.Id;
            this.MedicineName = row.MedicineName;
            this.Number = row.Number;
            this.Price = row.Price;
            this.ExpiryDate = row.ExpiryDate;
            this.IsHasTapes = row.IsHasTapes;
            this.NumberOfTapesInOneMedicine = row.NumberOfTapesInOneMedicine;
            this.TotaleNumberOfTapes = row.TotaleNumberOfTapes;
            this.TotalePrice = row.TotalePrice;
        }
        public int Id { get; set; }
        [Display(Name = "إسم الدواء")]
        public string MedicineName { get; set; }
        [Display(Name = "كمية الدواء المتبقية")]

        public int Number { get; set; }
        [Display(Name = "سعر الدواء أو سعر الشريط الواحد")]

        public int Price { get; set; }
        [Display(Name = "تاريخ إنتهاء الصلاحية")]
        public DateTime ExpiryDate { get; set; }

        [Display(Name = "هل هذا الدواء يحتوي علي أشرطة")]
        public Boolean IsHasTapes { get; set; }
        [Display(Name = "عدد الأشرطة في الدواء الواحد")]
        public int NumberOfTapesInOneMedicine { get; set; }
        [Display(Name = "عدد الأشرطة المتبقية")]
        public int TotaleNumberOfTapes { get; set; }
        [Display(Name = "قيمة المبلغ الكلي للدواء المتبقي ")]
        public int TotalePrice { get; set; }
    }
}