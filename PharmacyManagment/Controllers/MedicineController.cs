using PharmacyManagment.Models.Data;
using PharmacyManagment.Models.ViewModels.Medicine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PharmacyManagment.Controllers
{
    public class MedicineController : Controller
    {
        // GET: Medicine
        public ActionResult Index()
        {
            //check if user is login in App
            string username = User.Identity.Name;

            if (string.IsNullOrEmpty(username))
                return RedirectToAction("login", "Account");
            return RedirectToAction("AddMedicine" ,"Medicine");
        }
        // GET: /Medicine/AddMedicine
        [ActionName("AddMedicine")]
        [HttpGet]
        public ActionResult AddMedicine()
        {
            //check if user is login in App
            string username = User.Identity.Name;

            if (string.IsNullOrEmpty(username))
                return RedirectToAction("login", "Account");
            return View("AddMedicine");
        }

        // POST: /Medicine/AddMedicine
        [ActionName("AddMedicine")]
        [HttpPost]
        public ActionResult AddMedicine(MedicineVM model)
        {
            //check if user is login in App
            string username = User.Identity.Name;

            if (string.IsNullOrEmpty(username))
                return RedirectToAction("login", "Account");
            // Check model state
            if (!ModelState.IsValid)
            {
                return View("AddMedicine", model);
            }

            using (Db db = new Db())
            {
                // Make sure MedicineName is unique
                if (db.Medicines.Any(x => x.MedicineName.Equals(model.MedicineName)))
                {
                    ModelState.AddModelError("", "الدواء  " + model.MedicineName + " موجود مسبقا.");
                    model.MedicineName = "";
                    return View("AddMedicine", model);
                }
                int medicineTotalePrice , TotaleNumberOfTapesOfMedicine;
                if (model.IsHasTapes == true)
                {
                    TotaleNumberOfTapesOfMedicine = model.Number * model.NumberOfTapesInOneMedicine;
                    medicineTotalePrice = model.Price * TotaleNumberOfTapesOfMedicine;
                }
                else {
                    medicineTotalePrice = model.Price * model.Number;
                    TotaleNumberOfTapesOfMedicine = 0;
                    model.NumberOfTapesInOneMedicine = 0;
                }
                // Create userDTO
                MedicineDTO medicineDTO = new MedicineDTO()
                {

                    MedicineName = model.MedicineName,
                    Number = model.Number,
                    Price = model.Price,
                    ExpiryDate = model.ExpiryDate.Date,
                    IsHasTapes = model.IsHasTapes,
                    NumberOfTapesInOneMedicine = model.NumberOfTapesInOneMedicine,
                    TotalePrice = medicineTotalePrice  ,
                    TotaleNumberOfTapes = TotaleNumberOfTapesOfMedicine
                };

                // Add the DTO
                db.Medicines.Add(medicineDTO);

                // Save
                db.SaveChanges();

            }

            // Create a TempData message
            TempData["SM"] = "تمت إضافة  الدواء  بنجاح";

            // Redirect
            return RedirectToAction("AddMedicine");
        }
        // Get:  /Medicine/ShowMedicinesInfo 
        [HttpGet]
        public ActionResult ShowMedicines()
        {
            //check if user is login in App
            string username = User.Identity.Name;

            if (string.IsNullOrEmpty(username))
                return RedirectToAction("login", "Account");

            // Declare a list of Medicine
            List<MedicineVM> listOfMedicinesVM;
            DateTime today = DateTime.Now.Date;
            using (Db db = new Db())
                {
                    // Init the list
                    listOfMedicinesVM = db.Medicines.ToArray()
                                      .Select(x => new MedicineVM(x))
                                      .ToList();
                }
            return View(listOfMedicinesVM);
        }
        // Get:  /Medicine/ShowMedicinesOutOfDate
        public ActionResult ShowMedicinesOutOfDate()
        {

            // Declare a list of Medicine
            List<MedicineVM> listOfMedicinesVM;
            DateTime today = DateTime.Now.Date;
            using (Db db = new Db())
            {
                // Init the list
                listOfMedicinesVM = db.Medicines.Where(d => d.ExpiryDate <= today).ToArray()
                                  .Select(x => new MedicineVM(x))
                                  .ToList();
            }
            return View(listOfMedicinesVM);
        }
        public ActionResult ShowMedicinesEnded()
        {

            // Declare a list of Medicine
            List<MedicineVM> listOfMedicinesVM;
            using (Db db = new Db())
            {
                // Init the list
                listOfMedicinesVM = db.Medicines.Where(d => d.Number == 0).ToArray()
                                  .Select(x => new MedicineVM(x))
                                  .ToList();
            }
            return View(listOfMedicinesVM);
        }
        // Get:  /Medicine/ShowMedicinesOutOfDate
        public ActionResult ShowMedicinesOfExpiryDateLessThanOrEquel4Months()
        {

            // Declare a list of Medicine
            List<MedicineVM> listOfMedicinesVM;
            DateTime today = DateTime.Now.Date;
            DateTime toDayPlus4Months = DateTime.Now.Date.AddMonths(4);
            using (Db db = new Db())
            {
                // Init the list
                listOfMedicinesVM = db.Medicines.Where(d => d.ExpiryDate <= toDayPlus4Months && d.ExpiryDate > today).ToArray()
                                  .Select(x => new MedicineVM(x))
                                  .ToList();
            }
            return View(listOfMedicinesVM);
        }

        //Search Medicine to edit or delete or show detials 
        
        public ActionResult SearchMedicines(string SearhName) {
        
            // Declare a list of Medicine
            List<MedicineVM> listOfMedicinesVM;
            
                using (Db db = new Db())
                {
                    // Init the list
                    listOfMedicinesVM = db.Medicines.Where(a => a.MedicineName.Contains(SearhName)
                    || a.Price.ToString().Contains(SearhName)
                    || a.ExpiryDate.ToString().Contains(SearhName)
                    || a.Number.ToString().Contains(SearhName)
                    ).ToArray()
                                      .Select(x => new MedicineVM(x))
                                      .ToList();
                }
            // Return view with list
            return PartialView(listOfMedicinesVM);

        }
        // POST: /Medicine/DeleteMedicine/id
        public ActionResult DeleteMedicine(int id)
        {
           //check if user is login in App
            string username = User.Identity.Name;

            if (string.IsNullOrEmpty(username))
                return RedirectToAction("login", "Account");
            // Delete Medicine from DB
            using (Db db = new Db())
            {
                MedicineDTO dto = db.Medicines.Find(id);
                if (dto == null)
                {
                    return HttpNotFound();
                }
                db.Medicines.Remove(dto);

                db.SaveChanges();
            }
            // Redirect
            return RedirectToAction("ShowMedicines");
        }
        // Get:  /Medicine/EditMedicine
        [HttpGet]
        public ActionResult EditMedicine(int id)
        {
            //check if user is login in App
            string username = User.Identity.Name;

            if (string.IsNullOrEmpty(username))
                return RedirectToAction("login", "Account");
            // Declare productVM
            MedicineVM model;

            using (Db db = new Db())
            {
                // Get the Medicine
                MedicineDTO dto = db.Medicines.Find(id);

                // Make sure Medicine exists
                if (dto == null)
                {
                    return Content("هذا الدواء  غير موجود ");
                }

                // init model
                model = new MedicineVM(dto);
            }

            // Return view with model
            return View(model);
        }

        // POST: Medicine/EditMedicine/id
        [HttpPost]
        public ActionResult EditMedicine(MedicineVM model)
        {
            // Get Medicine id
            int id = model.Id;

            // Check model state
            
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "لم تتم عملية التعديل  ");
                return View(model);
            }
            
            // Make sure MedicineName  is unique
            using (Db db = new Db())
            {
                if (db.Medicines.Where(x => x.Id != id).Any(x => x.MedicineName == model.MedicineName))
                {
                    ModelState.AddModelError("", "شركة    " + model.MedicineName + " موجودة مسبقا.");
                    return View(model);
                }
            }
            int medicineTotalePrice, TotaleNumberOfTapesOfMedicine;
            if (model.IsHasTapes == true)
            {
                TotaleNumberOfTapesOfMedicine = model.Number * model.NumberOfTapesInOneMedicine;
                medicineTotalePrice = model.Price * TotaleNumberOfTapesOfMedicine;
            }
            else
            {
                medicineTotalePrice = model.Price * model.Number;
                TotaleNumberOfTapesOfMedicine = 0;
                model.NumberOfTapesInOneMedicine = 0;
            }

            // Update Medicine
            using (Db db = new Db())
            {
                MedicineDTO dto = db.Medicines.Find(id);

                dto.MedicineName = model.MedicineName;
                dto.Number = model.Number;
                dto.Price = model.Price;
                dto.ExpiryDate = model.ExpiryDate;
                dto.IsHasTapes = model.IsHasTapes;
                dto.NumberOfTapesInOneMedicine = model.NumberOfTapesInOneMedicine;
                dto.TotaleNumberOfTapes = TotaleNumberOfTapesOfMedicine;
                dto.TotalePrice = medicineTotalePrice;

                db.SaveChanges();
            }

            // Set TempData message
            TempData["SM"] = "تم تعديل بيانات الدواء  بنجاح ";
            // Redirect
            return RedirectToAction("EditMedicine");
        }

        // Get:  /Medicine/ShowMedicines
        public ActionResult DetialsMedicine(int id)
        {
            //check if user is login in App
            string username = User.Identity.Name;

            if (string.IsNullOrEmpty(username))
                return RedirectToAction("login", "Account");
            // Declare a list of Medicine
            MedicineDetialsVM model;
            using (Db db = new Db())
            {
                // Get the Medicine
                MedicineDTO dto = db.Medicines.Find(id);

                // Make sure Medicine exists
                if (dto == null)
                {
                    return Content("هذا الدواء  غير موجود ");
                }
                
                // init model
                model = new MedicineDetialsVM(dto);
                
            }
            // Return view with list
            return View(model);

        }

    }
}