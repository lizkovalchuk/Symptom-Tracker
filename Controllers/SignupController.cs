using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SymptomTrackerMVC.Models;
using System.Web.Security;

namespace SymptomTrackerMVC.Controllers
{
    public class SignupController : Controller
    {
        private STContext db = new STContext();

        // GET: Signup
        public ActionResult Index()
        {
            List<string> genders = new List<string>();
            List<string> type = new List<string>();
            genders.Add("male"); genders.Add("female"); genders.Add("other");
            type.Add("guest"); type.Add("admin");
            ViewBag.Genders = new SelectList(genders);
            ViewBag.Types = new SelectList(type);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Id,First_name,Last_name,Phone,Email,DOB,Sex,Weight,Height,type,password")] UserTable userTable)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.UserTables.Add(userTable);
                    db.SaveChanges();
                    Session["userId"] = userTable.Id;
                    Session["name"] = userTable.First_name;
                    //add formauth...
                    FormsAuthentication.SetAuthCookie(userTable.Email, false);
                    return RedirectToAction("Create", "Symptoms");
                }
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException;
            }
            return View("Index", "Symptoms");
        }
    }
}