using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PharmacyManagment.Models.Data
{
    [Table("OrderOuttbl")]
    public class OrderOutDTO
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime OrderDate { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual EmployeeDTO Employee { get; set; }
    }
}