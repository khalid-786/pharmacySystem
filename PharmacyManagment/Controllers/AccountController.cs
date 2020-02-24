using PagedList;
using PharmacyManagment.Models.Data;
using PharmacyManagment.Models.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PharmacyManagment.Controllers
{
    
    public class AccountController : Controller
    {
        // GET: Account

        public ActionResult Index()
        {
            return RedirectToAction("login", "Account");
        }

        // GET: /account/login
        [HttpGet]
        public ActionResult Login()
        {
            // Confirm user is not logged in

            string username = User.Identity.Name;
            

            if (!string.IsNullOrEmpty(username))
                return RedirectToAction("index" ,"Home");

            // Return view
            return View();
        }

        // POST: /account/login
        [HttpPost]
        public ActionResult Login(LoginUserVM model)
        {
            // Check model state
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Check if the user is valid

            bool isValid = false;

            using (Db db = new Db())
            {
                if (db.Employees.Any(x => x.Username.Equals(model.Username) && x.password.Equals(model.password)))
                {
                    isValid = true;
                }
            }

            if (!isValid)
            {
                ModelState.AddModelError("", "خطا في إسم المستخدم أو كلمة المرور.");
                return View(model);
            }
            else
            {
                FormsAuthentication.SetAuthCookie(model.Username, model.RememberMe);
                return Redirect(FormsAuthentication.GetRedirectUrl(model.Username, model.RememberMe));
            }
        }

        // GET: /account/create-account
        [ActionName("AddEmployee")]
        [HttpGet]
        public ActionResult AddEmployee()
        {
            return View("AddEmployee");
        }

        // POST: /account/create-account
        [ActionName("AddEmployee")]
        [HttpPost]
        public ActionResult AddEmployee(EmployeeVM model)
        {
            // Check model state
            if (!ModelState.IsValid)
            {
                return View("AddEmployee", model);
            }

            // Check if passwords match
            if (!model.password.Equals(model.ConfirmPassword))
            {
                ModelState.AddModelError("", "كلمة المرور غير مطابقة ");
                return View("AddEmployee", model);
            }

            using (Db db = new Db())
            {
                // Make sure username is unique
                if (db.Employees.Any(x => x.Username.Equals(model.Username)))
                {
                    ModelState.AddModelError("", "الموظف  " + model.Username + " موجود مسبقا.");
                    model.Username = "";
                    return View("AddEmployee", model);
                }

                // Create userDTO
                EmployeeDTO userDTO = new EmployeeDTO()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    Username = model.Username,
                    password = model.password
                   
                };
                
                // Add the DTO
                db.Employees.Add(userDTO);

                // Save
                db.SaveChanges();

                // Add to UserRolesDTO
                int id = userDTO.Id;

                UserRoleDTO userRolesDTO = new UserRoleDTO()
                {
                    UserId = id,
                    RoleId = 2
                };

                db.UserRoles.Add(userRolesDTO);
                db.SaveChanges();
            }

            // Create a TempData message
            TempData["SM"] = "تمت إضافة  بيانات الموظف بنجاح";

            // Redirect
            return RedirectToAction("AddEmployee");
        }

        // GET: /account/Logout
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("~/account/login");
        }

        // GET: /account/create-account
        [ActionName("AddAdmin")]
        [HttpGet]
        public ActionResult AddAdmin()
        {
            return View("AddAdmin");
        }

        // POST: /account/create-account
        [ActionName("AddAdmin")]
        [HttpPost]
        public ActionResult AddAdmin(EmployeeVM model)
        {
            // Check model state
            if (!ModelState.IsValid)
            {
                return View("AddAdmin", model);
            }

            // Check if passwords match
            if (!model.password.Equals(model.ConfirmPassword))
            {
                ModelState.AddModelError("", "كلمة المرور غير مطابقة ");
                return View("AddAdmin", model);
            }

            using (Db db = new Db())
            {
                // Make sure username is unique
                if (db.Employees.Any(x => x.Username.Equals(model.Username)))
                {
                    ModelState.AddModelError("", "الموظف  " + model.Username + " موجود مسبقا.");
                    model.Username = "";
                    return View("AddAdmin", model);
                }

                // Create userDTO
                EmployeeDTO userDTO = new EmployeeDTO()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    Username = model.Username,
                    password = model.password
                };

                // Add the DTO
                db.Employees.Add(userDTO);

                // Save
                db.SaveChanges();

                // Add to UserRolesDTO
                int id = userDTO.Id;

                UserRoleDTO userRolesDTO = new UserRoleDTO()
                {
                    UserId = id,
                    RoleId = 1
                };

                db.UserRoles.Add(userRolesDTO);
                db.SaveChanges();
            }

            // Create a TempData message
            TempData["SM"] = "تمت حفظ  بيانات المدير بنجاح";

            // Redirect
            return RedirectToAction("AddAdmin");
        }
        // GET: Admin/Pages
        public ActionResult ShowEmployee(int? page)
        {
            // Declare a list of ProductVM
            List<EmployeeVM> listOfEmployeetVM;

            // Set page number
            var pageNumber = page ?? 1;

            using (Db db = new Db())
            {
                // Init the list
                listOfEmployeetVM = db.Employees.ToArray()
                                  .Select(x => new EmployeeVM(x))
                                  .ToList();
            }


            // Set pagination
            var onePageOfEmployees = listOfEmployeetVM.ToPagedList(pageNumber, 3);
            ViewBag.OnePageOfEmployees = onePageOfEmployees;

            // Return view with list
            return View(listOfEmployeetVM);

        }
        [HttpGet]
        public ActionResult EditEmployee(int id)
        {
            // Declare productVM
            EmployeeVM model;

            using (Db db = new Db())
            {
                // Get the product
                EmployeeDTO dto = db.Employees.Find(id);

                // Make sure product exists
                if (dto == null)
                {
                    return Content("هذا الموظف غير موجود ");
                }

                // init model
                model = new EmployeeVM(dto);
            }

            // Return view with model
            return View(model);
        }

        // POST: Account/EditEmployee/id
        [HttpPost]
        public ActionResult EditEmployee(EmployeeVM model)
        {
            // Get product id
            int id = model.Id;

            // Check model state
            /*
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "لم تتم عملية التعديل  ");
                return View(model);
            }
            */
            // Make sure product name is unique
            using (Db db = new Db())
            {
                if (db.Employees.Where(x => x.Id != id).Any(x => x.Username == model.Username))
                {
                    ModelState.AddModelError("", "هذا الموظف موجود مسبقا ");
                    return View(model);
                }
            }

            // Update product
            using (Db db = new Db())
            {
                EmployeeDTO dto = db.Employees.Find(id);

                dto.FirstName = model.FirstName;
                dto.LastName = model.LastName;
                dto.PhoneNumber = model.PhoneNumber;
                dto.Username = model.Username;

                db.SaveChanges();
            }

            // Set TempData message
            TempData["SM"] = "تم تعديل بيانات الموظف بنجاح ";
            // Redirect
            return RedirectToAction("EditEmployee");
        }
        // GET: Account/DeleteEmployee/id
        public ActionResult DeleteEmployee(int id)
        {
            // Delete Employess from DB
            using (Db db = new Db())
            {
                EmployeeDTO dto = db.Employees.Find(id);
                db.Employees.Remove(dto);

                db.SaveChanges();
            }
            // Delete Employess from UserRoles in DB
            using (Db db = new Db())
            {
                UserRoleDTO dto = db.UserRoles.Where(x => x.UserId == id).SingleOrDefault();
                db.UserRoles.Remove(dto);

                db.SaveChanges();
            }



            // Redirect
            return RedirectToAction("ShowEmployee");
        }

        // GET: /account/AddCustomer
        [ActionName("AddCustomer")]
        [HttpGet]
        public ActionResult AddCustomer()
        {
            return View("AddCustomer");
        }

        // POST: /account/AddCustomer
        [ActionName("AddCustomer")]
        [HttpPost]
        public ActionResult AddCustomer(CustomerVM model)
        {
            // Check model state
            if (!ModelState.IsValid)
            {
                return View("AddCustomer", model);
            }

            using (Db db = new Db())
            {
                // Make sure Customer CompanyName is unique
                if (db.Customers.Any(x => x.CompanyName.Equals(model.CompanyName)))
                {
                    ModelState.AddModelError("", "شركة    " + model.CompanyName + " موجودة مسبقا.");
                    model.CompanyName = "";
                    return View("AddCustomer", model);
                }

                // Create customerDTO
                CustomerDTO customerDTO = new CustomerDTO()
                {
                    CustomerName = model.CustomerName,
                    CompanyName = model.CompanyName,
                    PhoneNumber = model.PhoneNumber,
                   
                };

                // Add the DTO
                db.Customers.Add(customerDTO);

                // Save
                db.SaveChanges();

           
            }

            // Create a TempData message
            TempData["SM"] = "تمت حفظ  بيانات العميل بنجاح ";

            // Redirect
            return RedirectToAction("AddCustomer");
        }
        public ActionResult ShowCustomers()
        {
            // Declare a list of Customer
            List<CustomerVM> listOfCustomersVM;
            using (Db db = new Db())
            {
                // Init the list
                listOfCustomersVM = db.Customers.ToArray()
                                  .Select(x => new CustomerVM(x))
                                  .ToList();
            }
            // Return view with list
            return View(listOfCustomersVM);

        }
        public ActionResult DeleteCustomer(int id)
        {

           /* if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }*/
           
            // Delete Customer from DB
            using (Db db = new Db())
            {
                CustomerDTO dto = db.Customers.Find(id);
                if (dto == null)
                {
                    return HttpNotFound();
                }
                db.Customers.Remove(dto);

                db.SaveChanges();
            }
            // Redirect
            return RedirectToAction("ShowCustomers");
        }
        // GET: Account/EditCustomer/id
        [HttpGet]
        public ActionResult EditCustomer(int id)
        {
            /*if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }*/
            // Declare productVM
            CustomerVM model;

            using (Db db = new Db())
            {
                // Get the product
                CustomerDTO dto = db.Customers.Find(id);

                // Make sure product exists
                if (dto == null)
                {
                    return Content("هذا العميل  غير موجود ");
                }

                // init model
                model = new CustomerVM(dto);
            }

            // Return view with model
            return View(model);
        }

        // POST: Account/EditCustomer/id
        [HttpPost]
        public ActionResult EditCustomer(CustomerVM model)
        {
            // Get Customer id
            int id = model.Id;

            // Check model state
            /*
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "لم تتم عملية التعديل  ");
                return View(model);
            }
            */
            // Make sure Customer  Companyname is unique
            using (Db db = new Db())
            {
                if (db.Customers.Where(x => x.Id != id).Any(x => x.CompanyName == model.CompanyName))
                {
                    ModelState.AddModelError("", "شركة    " + model.CompanyName + " موجودة مسبقا.");
                    return View(model);
                }
            }

            // Update Customer
            using (Db db = new Db())
            {
                CustomerDTO dto = db.Customers.Find(id);

                dto.CustomerName = model.CustomerName;
                dto.CompanyName = model.CompanyName;
                dto.PhoneNumber = model.PhoneNumber;

                db.SaveChanges();
            }

            // Set TempData message
            TempData["SM"] = "تم تعديل بيانات العميل  بنجاح ";
            // Redirect
            return RedirectToAction("EditCustomer");
        }
    }
}