using PharmacyManagment.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PharmacyManagment.Models.ViewModels.Medicine
{
    public class MedicineVM
    {
        public MedicineVM() { }

        public MedicineVM(MedicineDTO row) {
            this.Id = row.Id;
            this.MedicineName = row.MedicineName;
            this.Number = row.Number;
            this.Price = row.Price;
            this.ExpiryDate = row.ExpiryDate;
            this.IsHasTapes = row.IsHasTapes;
            this.NumberOfTapesInOneMedicine = row.NumberOfTapesInOneMedicine;
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "  رجاءاإدخل إسم الدواء المراد إضافته ")]
        [Display(Name = "إسم الدواء")]

        public string MedicineName { get; set; }
        [Required(ErrorMessage = "  رجاءاإدخل كمية الدواء الذي تريد إضافته")]
        [Display(Name = "كمية الدواء")]

        public int Number { get; set; }
        [Required(ErrorMessage = "  رجاءاإدخل سعر الدواء أو سعر الشريط الواحد")]
        [Display(Name = "سعر الدواء أو سعر الشريط الواحد")]

        public int Price { get; set; }
        [Required(ErrorMessage = "  رجاءاإدخل تاريخ إنتهاء صلاحية الدواء")]
        [Display(Name = "تاريخ إنتهاء الصلاحية")]
        public DateTime ExpiryDate { get; set; }

        [Display(Name = "هل هذا الدواء يحتوي علي أشرطة")]
        public Boolean IsHasTapes { get; set; }
       [Required(ErrorMessage = "  إذا كان الدواء لايحتوي علي أشرطة ضع قيمة هذا الحقل صفر")]
        [Display(Name = "عدد الأشرطة في الدواء الواحد")]
        public int NumberOfTapesInOneMedicine { get; set; }
    }
}