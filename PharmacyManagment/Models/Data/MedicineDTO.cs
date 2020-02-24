using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PharmacyManagment.Models.Data
{
    [Table("Medicinetbl")]
    public class MedicineDTO
    {
        [Key]
        public int Id { get; set; }
        public string MedicineName { get; set; }
        public int Number { get; set; }
        public int Price { get; set; }
        public DateTime ExpiryDate { get; set; }
        public Boolean IsHasTapes { get; set; }
        public int NumberOfTapesInOneMedicine { get; set; }
        public int TotalePrice { get; set; }
        public int TotaleNumberOfTapes { get; set; }
    }
}