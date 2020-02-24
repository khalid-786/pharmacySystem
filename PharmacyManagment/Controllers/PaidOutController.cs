using Microsoft.Ajax.Utilities;
using PharmacyManagment.Models.Data;
using PharmacyManagment.Models.ViewModels.Expense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PharmacyManagment.Controllers
{
    public class PaidOutController : Controller
    {
        // GET: PaidOut
        public ActionResult Index()
        {
            return RedirectToAction("AddExpense", "PaidOut");
        }

        // GET: /PaidOut/AddExpense
        [ActionName("AddExpense")]
        [HttpGet]
        public ActionResult AddExpense()
        {
            string username = User.Identity.Name;


            if (string.IsNullOrEmpty(username))
                return RedirectToAction("login", "Account");
            return View("AddExpense");
        }

        // POST: /PaidOut/AddExpense
        [ActionName("AddExpense")]
        [HttpPost]
        public ActionResult AddExpense(ExpenseVM model)
        {
            // Check model state
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "الصرفية       موجودة مسبقا  الرجاء إختيار عنوان أخر لهذه الصرفية");
                return View("AddExpense", model);
            }
            string username = User.Identity.Name;

            using (Db db = new Db())
            {
                // Make sure expense Title is unique
                if (db.Expenses.Any(x => x.Expense_Title.Equals(model.Expense_Title)))
                {
                    ModelState.AddModelError("", "الصرفية     " + model.Expense_Title + " موجودة مسبقا  الرجاء إختيار عنوان أخر لهذه الصرفية");
                    model.Expense_Title = "";
                    return View("AddExpense", model);
                }
                EmployeeDTO dto = db.Employees.FirstOrDefault(x => x.Username == username);
                //Create expenseDTO
                ExpenseDTO expenseDTO = new ExpenseDTO()
                {
                    Expense_Title = model.Expense_Title,
                    Expense_Date = DateTime.Now,
                    UserId = dto.Id ,
                    Expense_Quantitative = model.Expense_Quantitative,
                    Expense_Day = DateTime.Now.Date

                };

                // Add the DTO
                db.Expenses.Add(expenseDTO);

                // Save
                db.SaveChanges();


            }

            // Create a TempData message
            TempData["SM"] = "تمت حفظ  بيانات الصرفية بنجاح ";

            // Redirect
            return RedirectToAction("AddExpense");
        }

        public ActionResult ShowToDayExpense() {

           
            // Declare a list of ToDayExpense
            //where expense.Expense_Date.ToString().Contains(today) 
            List<ExpenseDetialsVM> listOfExpenseDetialsVM;
            DateTime today = DateTime.Now.Date;
            using (Db db = new Db())
            {
                
                var ex = from expense in db.Expenses
                           join user in db.Employees
                           on expense.UserId equals user.Id
                         where expense.Expense_Day == today
                         select new ExpenseDetialsVM() {
                                  Id = expense.Id ,
                                  UserName = user.FirstName+" "+user.LastName ,
                                  Expense_Date = expense.Expense_Date ,
                                  Expense_Quantitative = expense.Expense_Quantitative ,
                                  Expense_Title = expense.Expense_Title
                              };
                listOfExpenseDetialsVM = ex.ToList();
            }
           
            // Return view with list
            return View(listOfExpenseDetialsVM);
            
        }
    }
}