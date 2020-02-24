using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PharmacyManagment.Models.Data
{
    [Table("customerstbl")]
    public class CustomerDTO
    {
        [Key]
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
    }
}