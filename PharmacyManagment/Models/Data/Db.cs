using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace PharmacyManagment.Models.Data
{
    public class Db: DbContext
    {
        public DbSet<EmployeeDTO> Employees { get; set; }
        public DbSet<CustomerDTO> Customers { get; set; }
        public DbSet<RoleDTO> Roles { get; set; }
        public DbSet<UserRoleDTO> UserRoles { get; set; }
        public DbSet<MedicineDTO> Medicines { get; set; }
        public DbSet<OrderOutDTO> OrderOut { get; set; }
        public DbSet<OrderOutDetialsDTO> OrderOutDetials { get; set; }
        public DbSet<ExpenseDTO> Expenses { get; set; }
        public DbSet<OrderInDTO> OrderIn { get; set; }
        public DbSet<OrderInDetialsDTO> OrderInDetials { get; set; }

        public System.Data.Entity.DbSet<PharmacyManagment.Models.ViewModels.OrderIn.OrderInDetialsVM> OrderInDetialsVMs { get; set; }

        public System.Data.Entity.DbSet<PharmacyManagment.Models.ViewModels.OrderOut.OrderOutDetialsInDate> OrderOutDetialsInDates { get; set; }

        public System.Data.Entity.DbSet<PharmacyManagment.Models.ViewModels.OrderOut.OrderOutDetialsVM> OrderOutDetialsVMs { get; set; }

        public System.Data.Entity.DbSet<PharmacyManagment.Models.ViewModels.Medicine.MedicineVM> MedicineVMs { get; set; }
    }
}