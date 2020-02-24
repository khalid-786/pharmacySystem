using PharmacyManagment.Models.Data;
using PharmacyManagment.Models.ViewModels.Account;
using PharmacyManagment.Models.ViewModels.OrderIn;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace PharmacyManagment.Controllers
{
    public class OrderInController : Controller
    {
        // GET: OrderIn
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult printOrderAction(int id)
        {// ,  int id

            var report = new ActionAsPdf("printOrderInfo" , new { id = id });
           return report;
        }
        public ActionResult printOrderInfo(int id) {
            List<OrderInDetialsVM> listOrderInDetials;
            OrderInVM model;
            using (Db db = new Db())
            {
                listOrderInDetials = db.OrderInDetials.Where(x => x.OrderId == id)
                    .ToArray().Select(x => new OrderInDetialsVM(x)).ToList();
                OrderInDTO dto = db.OrderIn.Find(id);
                model = new OrderInVM(dto);
            }
            ViewBag.OrderDate = model.OrderDate;
            ViewBag.Paidprice = model.PaidPrice;
            ViewBag.Unpaidprice = model.UnpaidPrice;
            ViewBag.payementDate = model.PayementDate.ToShortDateString();

            return View(listOrderInDetials);
        }
        public ActionResult receiptOrderIn()
        {
            /*init model7
            OrderInVM model = new OrderInVM();
            //add the list of category to model
            using (Db db = new Db())
            {
                model.Customeres = new SelectList(db.Customers.ToList(), "Id", "Name");
            }

            //return view 
            return View(model);*/

            return View();
        }
        public ActionResult UnPaidOrder()
        {
            List<OrderInVM> listOrderInUnPiad;
            using (Db db = new Db()) {
                listOrderInUnPiad = db.OrderIn.Where(x => x.UnpaidPrice != 0)
                    .ToArray().Select(x => new OrderInVM(x)).ToList();
            }
                return View(listOrderInUnPiad);
        }
        [HttpGet]
        public ActionResult paymentOrderIn(int id)
        {
            //check if user is login in App
            string username = User.Identity.Name;

            if (string.IsNullOrEmpty(username))
                return RedirectToAction("login", "Account");
            // Declare productVM
            OrderInVM model;

            using (Db db = new Db())
            {
                // Get the OrderIn
                OrderInDTO dto = db.OrderIn.Find(id);

                // Make sure OrderIn exists
                if (dto == null)
                {
                    return Content("هذا الفاتورة  غير موجودة ");
                }

                // init model
                model = new OrderInVM(dto);
            }

            // Return view with model
            return View(model);
        }

        // POST: OrderIn/paymentOrderIn/id
        [HttpPost]
        public ActionResult paymentOrderIn(OrderInVM model)
        {
            // Get OrderIn id
            long id = model.Id;

            // Check model state

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "لم تتم عملية التعديل  ");
                return View(model);
            }
            if (model.PaidPrice > model.UnpaidPrice)
            {
                ModelState.AddModelError("", "المبلغ الدخل أكثر من المبلغ المتبقي");
                return View(model);
            }else { 
            // Update OrdrIn
            using (Db db = new Db())
            {
                
                    OrderInDTO dto = db.OrderIn.Find(id);
                    dto.PaidPrice += model.PaidPrice;
                    dto.UnpaidPrice = dto.TotalePrice - dto.PaidPrice;
              
                    if (model.TotalePrice == model.PaidPrice)
                    {
                        dto.PayementDate = DateTime.Now.Date;
                    }
                    db.SaveChanges();
                
                
            }

            // Set TempData message
            TempData["SM"] = "تم عملية السداد  بنجاح ";

            }
            // Redirect
            return RedirectToAction("paymentOrderIn");
        }

        // Get:  /OrderIn/OrderInDetials
        public ActionResult OrderInDetials(int id)
        {
            //check if user is login in App
            string username = User.Identity.Name;

            if (string.IsNullOrEmpty(username))
                return RedirectToAction("login", "Account");
            // Declare a list of OrderInDetials
           List<OrderInDetialsVM> model;
            using (Db db = new Db())
            {
                // Get the Medicine
                model = db.OrderInDetials.Where(x => x.OrderId == id).ToArray().Select(x => new OrderInDetialsVM(x)).ToList();

                // Make sure OrderInDetials exists
                if (model == null)
                {
                    return Content("هذه الفاتورة  غير موجودة ");
                }

            }
            // Return view with list
            return View(model);

        }
        public long AddOrderIn()
        {
            long id;
            using (Db db = new Db())
            {
                // Get the Medicine
                var dto = db.OrderIn.Select(x => x.Id);
                // Make sure OrderOut exists
                if (dto == null)
                {
                    id = 1;
                }
                else
                {
                    id = dto.Max() + 1 ;
                }
                // Create OrderInDTO
                OrderInDTO orderInDTO = new OrderInDTO()
                {
                    OrderDate = DateTime.Now,
                    OrderId = id
                   
                };
                // Add the DTO
                db.OrderIn.Add(orderInDTO);
                // Save
                db.SaveChanges();  
            }
            return id;
        }

        public string buyMedicine(int medicine_id = 0 , int medicine_order_id = 0, int medicine_QTY = 0,
         int medicine_price = 0 , int totalAmount = 0, Boolean medicine_isContainTapes = false, int medicine_OrderDetials_id = 0,
           int medicine_old_QTY = 0
          )
        {
            long id;
            if (medicine_OrderDetials_id == 0)
            {
                using (Db db = new Db())
                {
                    var n = db.Medicines.Where(x => x.Id == medicine_id).Select(x => x.MedicineName).First();
                    // Create OrderOutDTO
                    OrderInDetialsDTO orderInDTO = new OrderInDetialsDTO()
                    {
                        OrderId = medicine_order_id,
                        MedicineId = medicine_id ,
                        MedicineName = n ,
                        MedicineQuantity = medicine_QTY ,
                        MedicinePrice = medicine_price ,
                        TotalPrice = totalAmount

                    };

                    // Add the DTO
                    db.OrderInDetials.Add(orderInDTO);

                    // Save
                    db.SaveChanges();

                }
                // edit medicine data
                using (Db db = new Db())
                {
                    MedicineDTO dto = db.Medicines.Find(medicine_id);
                    if (medicine_isContainTapes)
                    {
                        dto.Number = (dto.TotaleNumberOfTapes + (medicine_QTY * dto.NumberOfTapesInOneMedicine)) / dto.NumberOfTapesInOneMedicine;
                        dto.TotaleNumberOfTapes += (medicine_QTY * dto.NumberOfTapesInOneMedicine);
                        dto.TotalePrice += ((medicine_QTY * dto.NumberOfTapesInOneMedicine) * dto.Price);
                    }
                    else
                    {
                        dto.Number += medicine_QTY;
                        dto.TotalePrice += (medicine_QTY * dto.Price);
                    }

                    

                    db.SaveChanges();
                }
                //end edit medicine

                //get last orderoutdetials id

                using (Db db = new Db())
                {
                    // Get the OrderOut
                    id = db.OrderInDetials.ToArray().Select(x => x.Id).Last();
                };
            }
            //edit medicine OrderOutDetials info
            else
            {
                // edit medicine data
                using (Db db = new Db())
                {
                    OrderInDetialsDTO orderDTO = db.OrderInDetials.Find(medicine_OrderDetials_id);
                    orderDTO.MedicineQuantity = medicine_QTY;
                    orderDTO.TotalPrice = totalAmount;
                    orderDTO.MedicinePrice = medicine_price;

                    MedicineDTO dto = db.Medicines.Find(medicine_id);
                    if (medicine_isContainTapes)
                    {
                        dto.TotaleNumberOfTapes = (dto.NumberOfTapesInOneMedicine * medicine_QTY) + medicine_old_QTY;
                        dto.Number = (dto.TotaleNumberOfTapes) / dto.NumberOfTapesInOneMedicine;
                        dto.TotalePrice = ((dto.NumberOfTapesInOneMedicine * medicine_QTY) + medicine_old_QTY) * dto.Price;
                    }
                    else
                    {
                        dto.Number = medicine_old_QTY+ medicine_QTY;
                        dto.TotalePrice = (medicine_QTY + medicine_old_QTY) * dto.Price;
                    }

                   // dto.TotalePrice = (dto.TotalePrice + medicine_old_totaleAmount) - totalAmount;

                    db.SaveChanges();
                }
                id = medicine_OrderDetials_id;
            }
            return id.ToString();
        }

        public string backbuymedicine(int med_ID, int order_ID, int payment_QTY)
        {
            // Delete OrderOutDetials from DB
            string data;
            using (Db db = new Db())
            {
                OrderInDetialsDTO dto = db.OrderInDetials.Find(order_ID);
                if (dto == null)
                {
                    data = "البيانات غير موجودة";
                }
                else
                {
                    db.OrderInDetials.Remove(dto);
                    db.SaveChanges();
                    MedicineDTO mdto = db.Medicines.Find(med_ID);
                    if (mdto != null)
                    {
                        if (mdto.IsHasTapes)
                        {
                            mdto.Number = (mdto.TotaleNumberOfTapes - (payment_QTY * mdto.NumberOfTapesInOneMedicine)) / mdto.NumberOfTapesInOneMedicine;
                            mdto.TotaleNumberOfTapes -= (payment_QTY * mdto.NumberOfTapesInOneMedicine);
                            mdto.TotalePrice -= ((payment_QTY * mdto.NumberOfTapesInOneMedicine) * mdto.Price);
                        }
                        else
                        {
                            mdto.Number -= payment_QTY;
                            mdto.TotalePrice -= (payment_QTY * mdto.Price);
                        }

                        

                        db.SaveChanges();
                    }
                    data = "تمت عملية التراجع بنجاح";
                }


            }
            return data;
        }

        public void saveOrderIn(int order_id, int Medicine_Quantity, int orderPrice, int Paid_Price, DateTime PayementDate) {
            // Update Medicine
            using (Db db = new Db())
            {
                OrderInDTO dto = db.OrderIn.Find(order_id);

                
                dto.TotalePrice = orderPrice;
                dto.TotaleQuantity = Medicine_Quantity;
                dto.PaidPrice = Paid_Price;
                dto.UnpaidPrice = orderPrice - Paid_Price;
                if (Paid_Price == orderPrice)
                {
                    dto.PayementDate = DateTime.Now.Date;
                }
                else {
                        dto.PayementDate = PayementDate.Date;
                }

                db.SaveChanges();
            }


        }

    }
}