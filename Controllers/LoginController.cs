using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using SymptomTrackerMVC.Controllers;
using SymptomTrackerMVC.Models;
using System.Web.Security;

namespace SymptomTrackerMVC.Controllers
{
    public class LoginController : Controller
    {

        STContext db = new STContext();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        //POST : Login

        [HttpPost]
        public ActionResult Index(UserTable userT)
        {

            List<UserTable> u = db.UserTables.Where(ut => ut.Email == userT.Email
                                                    && 
                                                    ut.password == userT.password).ToList();

            if (u.Count > 0)
            {
                Session["userId"] = u[0].Id;
                Session["name"] = u[0].First_name;
                
               
                //add formauth...
                FormsAuthentication.SetAuthCookie(userT.Email, false);

                if (u[0].type == "admin")
                {
                    return RedirectToAction("Index", "UserTables");
                }
                else if (u[0].type == "guest")
                {
                    return RedirectToAction("Index", "Symptoms");
                }
            }
            //FINAL RETURN
            return View(userT); 
        }

        //GET

        public ActionResult Logout()
        {
            if (Session["userId"] != null)
            {
                Session.Abandon();
            }
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

    }
}