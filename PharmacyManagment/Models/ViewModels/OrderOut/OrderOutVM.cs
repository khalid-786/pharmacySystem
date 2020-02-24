using PharmacyManagment.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PharmacyManagment.Models.ViewModels.OrderOut
{
    public class OrderOutVM
    {
        public OrderOutVM() { }
        public OrderOutVM(OrderOutDTO row) {
            this.Id = row.Id;
            this.OrderId = row.OrderId;
        }
        
        public int Id { get; set; }
        [Display(Name = "رقم الفاتورة الجديدة")]
        public int OrderId { get; set; }

    }
}