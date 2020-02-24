using PharmacyManagment.Models.Data;
using PharmacyManagment.Models.ViewModels.Medicine;
using PharmacyManagment.Models.ViewModels.OrderOut;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PharmacyManagment.Controllers
{
    public class OrderOutController : Controller
    {
        // GET: OrderOut
        public ActionResult Index()
        {
            string username = User.Identity.Name;
            if (string.IsNullOrEmpty(username))
                return RedirectToAction("login", "Account");
            return RedirectToAction("OrderOut", "AddOrderOut"); ;
        }
        public int AddOrderOutInfo()
        {
            string username = User.Identity.Name;
            int id;
            using (Db db = new Db())
            {
                // Get the Medicine
                var dto = db.OrderOut.Select(x => x.Id);
                EmployeeDTO userDTO = db.Employees.FirstOrDefault(x => x.Username == username);
                // Make sure OrderOut exists
                if (dto == null)
                {
                    id = 1;
                }
                else
                {
                    id = dto.Max() + 1;
                }
                // Create OrderInDTO
                OrderOutDTO orderOutDTO = new OrderOutDTO()
                {
                    OrderDate = DateTime.Now,
                    OrderId = id,
                    EmployeeId = 4

                };
                // Add the DTO
                db.OrderOut.Add(orderOutDTO);
                // Save
                db.SaveChanges();
            }
            return id;
        }
        public ActionResult printCurentOrderOutAction(int id)
        {// ,  int id

            var report = new ActionAsPdf("curentOrderOutInfoToPrint", new { id = id });
            return report;
        }
        public ActionResult curentOrderOutInfoToPrint(int id)
        {
            string username = User.Identity.Name;
            if (string.IsNullOrEmpty(username))
                return RedirectToAction("login", "Account");

            List<OrderOutDetialsVM> listOrderOutDetials;
            using (Db db = new Db())
            {
                listOrderOutDetials = db.OrderOutDetials.Where(x => x.OrderId == id)
                    .ToArray().Select(x => new OrderOutDetialsVM(x)).ToList();
                OrderOutDTO dto = db.OrderOut.Find(id);
            }

            return View(listOrderOutDetials);
        }
        public ActionResult searchOldOrderOut() {
            string username = User.Identity.Name;
            if (string.IsNullOrEmpty(username))
                return RedirectToAction("login", "Account");
            return View("searchOldOrderOut");
        }
        public ActionResult ShowOrderOutToDay(string AutoSearch ,DateTime orderDate)
        {
            string username = User.Identity.Name;
            if (string.IsNullOrEmpty(username))
                return RedirectToAction("login", "Account");

            
            DateTime thday = orderDate.Date;
            List<OrderOutDetialsVM> listOrderOutDetials;
            using (Db db = new Db())
            {
                listOrderOutDetials = db.OrderOutDetials.Where(x => x.MedicineName.Contains(AutoSearch) && x.OrderDay == thday)
                    .ToArray().Select(x => new OrderOutDetialsVM(x)).ToList();
               // OrderOutDTO dto = db.OrderOut.Find(id);
            }
            return PartialView(listOrderOutDetials);

        }
        [HttpGet]
        public ActionResult AddOrderOut()
        {
            string username = User.Identity.Name;
            if (string.IsNullOrEmpty(username))
                return RedirectToAction("login", "Account");
            return View("AddOrderOut");
        }
       
        //string AutoSearch
        public ActionResult searchMedicines(string AutoSearch)
        {
            // Declare a list of Medicine
            List<MedicineDetialsVM> listOfMedicinesVM;

            using (Db db = new Db())
            {

                // Init the list
                listOfMedicinesVM = db.Medicines.Where(x => x.MedicineName.Contains(AutoSearch)).ToArray()
                                  .Select(x => new MedicineDetialsVM(x))
                                  .ToList();
            }
            return PartialView(listOfMedicinesVM);
            // return AutoSearch;
        }

        public ActionResult searchMedicine(string AutoSearch)
        {
            // Declare a list of Medicine
            List<MedicineDetialsVM> listOfMedicinesVM;

            using (Db db = new Db())
            {

                // Init the list
                listOfMedicinesVM = db.Medicines.Where(x => x.MedicineName.Contains(AutoSearch)).ToArray()
                                  .Select(x => new MedicineDetialsVM(x))
                                  .ToList();
            }
            return Json(listOfMedicinesVM, JsonRequestBehavior.AllowGet);
            // return AutoSearch;
        }

        public string payMedicine(int medicine_id = 0, int medicine_order_id = 0, int medicine_QTY = 0,
            int totalAmount = 0, Boolean medicine_isContainTapes = false, int medicine_OrderDetials_id = 0,
            int medicine_old_totaleAmount = 0, int medicine_old_QTY = 0
            )
        {
            int id;
            if (medicine_OrderDetials_id == 0)
            {
                using (Db db = new Db())
                {
                    var mName = db.Medicines.Where(x => x.Id == medicine_id).Select(x => x.MedicineName).First();
                    var mPrice = db.Medicines.Where(x => x.Id == medicine_id).Select(x => x.Price).First();
                    // Create OrderOutDTO
                    OrderOutDetialsDTO orderoutDTO = new OrderOutDetialsDTO()
                    {
                        OrderId = medicine_order_id,
                        MedicineId = medicine_id,
                        OrderDate = DateTime.Now,
                        TotaleAmount = totalAmount,
                        QTY = medicine_QTY ,
                        OrderDay  = DateTime.Now.Date ,
                        MedicineName = mName ,
                        MedicinePrice = mPrice

                    };
                    ;
                    // Add the DTO
                    db.OrderOutDetials.Add(orderoutDTO);

                    // Save
                    db.SaveChanges();
                    id = orderoutDTO.Id;




                }
                // edit medicine data
                using (Db db = new Db())
                {
                    MedicineDTO dto = db.Medicines.Find(medicine_id);
                    if (medicine_isContainTapes)
                    {
                        dto.Number = (dto.TotaleNumberOfTapes - medicine_QTY) / dto.NumberOfTapesInOneMedicine;
                        dto.TotaleNumberOfTapes -= medicine_QTY;

                    }
                    else
                    {
                        dto.Number -= medicine_QTY;
                    }

                    dto.TotalePrice -= totalAmount;

                    db.SaveChanges();
                   // id = db.OrderOutDetials.ToArray().Select(x => x.Id).Last();
                }
                //end edit medicine

                //get last orderoutdetials id

                /* using (Db db = new Db())
                 {
                     // Get the OrderOut
                     id = db.OrderOutDetials.ToArray().Select(x => x.Id).Last();
                 };*/
               
            }
            //edit medicine OrderOutDetials info
            else
            {
                // edit medicine data
                using (Db db = new Db())
                {
                    OrderOutDetialsDTO orderDTO = db.OrderOutDetials.Find(medicine_OrderDetials_id);
                    orderDTO.QTY = medicine_QTY;
                    orderDTO.TotaleAmount = totalAmount;

                    MedicineDTO dto = db.Medicines.Find(medicine_id);
                    if (medicine_isContainTapes)
                    {
                        dto.TotaleNumberOfTapes = (dto.TotaleNumberOfTapes + medicine_old_QTY) - medicine_QTY;
                        dto.Number = (dto.TotaleNumberOfTapes) / dto.NumberOfTapesInOneMedicine;

                    }
                    else
                    {
                        dto.Number = (dto.Number + medicine_old_QTY) - medicine_QTY;
                    }

                    dto.TotalePrice = (dto.TotalePrice + medicine_old_totaleAmount) - totalAmount;

                    db.SaveChanges();
                }
                id = medicine_OrderDetials_id;
            }
            return id.ToString();
        }

        public string backpaymedicine(int med_ID, int order_ID, int payment_QTY, int payment_total_price)
        {
            // Delete OrderOutDetials from DB
            string data;
            using (Db db = new Db())
            {
                OrderOutDetialsDTO dto = db.OrderOutDetials.Find(order_ID);
                if (dto == null)
                {
                    data = "البيانات غير موجودة";
                }
                else
                {
                    db.OrderOutDetials.Remove(dto);
                    db.SaveChanges();
                    MedicineDTO mdto = db.Medicines.Find(med_ID);
                    if (mdto != null)
                    {
                        if (mdto.IsHasTapes)
                        {
                            mdto.Number = (mdto.TotaleNumberOfTapes + payment_QTY) / mdto.NumberOfTapesInOneMedicine;
                            mdto.TotaleNumberOfTapes += payment_QTY;

                        }
                        else
                        {
                            mdto.Number += payment_QTY;
                        }

                        mdto.TotalePrice += payment_total_price;

                        db.SaveChanges();
                    }
                    data = "تمت عملية التراجع بنجاح";
                }


            }
            return data;
        }
       
    }
}