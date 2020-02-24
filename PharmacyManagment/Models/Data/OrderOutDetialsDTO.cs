using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PharmacyManagment.Models.Data
{
    [Table("OrderOutDetialstbl")]
    public class OrderOutDetialsDTO
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
        public int MedicinePrice { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime OrderDay { get; set; }
        public int TotaleAmount { get; set; }
        public int QTY { get; set; }
        [ForeignKey("OrderId")]
        public virtual OrderOutDTO OrderOut { get; set; }
        [ForeignKey("MedicineId")]
        public virtual MedicineDTO Medicine { get; set; }
    }
}