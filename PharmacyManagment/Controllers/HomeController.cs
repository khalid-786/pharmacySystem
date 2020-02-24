using PharmacyManagment.Models.Data;
using PharmacyManagment.Models.ViewModels.Expense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PharmacyManagment.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DateTime today = DateTime.Now.Date;
            int total_medicine = 0, total_medicine_price =0 ,medicineOutOfDate =0 ,
                medicineRemain4Month  =0 ,UnpaidExpence =0 ,endMedicine =0
                ;
            DateTime toDayPlus4Months = DateTime.Now.Date.AddMonths(4);
            using (Db db = new Db()) {
                endMedicine = db.Medicines.Where(d => d.Number == 0).Count();
                total_medicine = db.Medicines.Select(m => m.Number).Sum();
                total_medicine_price = db.Medicines.Select(m => m.TotalePrice).Sum();
                medicineOutOfDate = db.Medicines.Where(d => d.ExpiryDate <= today).Count();
                UnpaidExpence = db.OrderIn.Where(x => x.UnpaidPrice != 0).Count();
                medicineRemain4Month = db.Medicines.Where(d => d.ExpiryDate <= toDayPlus4Months && d.ExpiryDate > today).Count();
                

            };
            ViewBag.totalMedicine = total_medicine;
            ViewBag.totalMedicinePrice = total_medicine_price;
            ViewBag.medicineOutOfDate = medicineOutOfDate;
            ViewBag.medicineRemain4Month = medicineRemain4Month;
            ViewBag.endMedicine = endMedicine;
            ViewBag.UnpaidExpence = UnpaidExpence;
            /* if (total_day_pay_medicie != 0)
             {
                 ViewBag.totalMedicineDayPay = total_day_pay_medicie;
             }
             else {
                 ViewBag.totalMedicineDayPay = 0;
             }*/
            string username = User.Identity.Name;


            if (!string.IsNullOrEmpty(username))
                ViewBag.UserNameLogin = username;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}