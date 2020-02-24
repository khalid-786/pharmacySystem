using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PharmacyManagment.Models.Data
{
    [Table("OrderIntbl")]
    public class OrderInDTO
    {
        [Key]
        public long Id { get; set; }
        public long OrderId { get; set; }
        public int TotalePrice { get; set; }
        public int TotaleQuantity { get; set; }
        public int PaidPrice { get; set; }
        public int UnpaidPrice { get; set; }
        public DateTime OrderDate { get; set; }

        public DateTime PayementDate { get; set; }
    }
}