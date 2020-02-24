using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PharmacyManagment.Models.Data
{
    [Table("OrderInDetialstbl")]
    public class OrderInDetialsDTO
    {
        [Key]
        public long Id { get; set; }
        public long OrderId { get; set; }
        
        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
        public int MedicineQuantity { get; set; }
        public int MedicinePrice { get; set; }
        public int TotalPrice { get; set; }
        [ForeignKey("OrderId")]
        public virtual OrderInDTO OrderIn { get; set; }
        [ForeignKey("MedicineId")]
        public virtual MedicineDTO Medicine { get; set; }
        

    }
}