using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SymptomTrackerMVC.Models;

namespace SymptomTrackerMVC.Controllers
{
    public class UserTablesController : Controller
    {
        private STContext db = new STContext();


//================= READ of CRUD =======================================//
//======================================================================//


        // GET: UserTables
        public ActionResult Index()
        {
            return View(db.UserTables.ToList());
        }

        // GET: UserTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTable userTable = db.UserTables.Find(id);
            if (userTable == null)
            {
                return HttpNotFound();
            }
            return View(userTable);
        }

//================= CREATE of CRUD =====================================//
//======================================================================//


        // GET: UserTables/Create
        [Authorize]
        public ActionResult Create()
        {
            List<string> genders = new List<string>();
            List<string> type = new List<string>();
            genders.Add("male"); genders.Add("female"); genders.Add("other");
            type.Add("guest"); type.Add("admin");
            ViewBag.Genders = new SelectList(genders);
            ViewBag.Types = new SelectList(type);
            return View("~/Views/UserTables/Create.cshtml");
        }

        // POST: UserTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,First_name,Last_name,Phone,Email,DOB,Sex,Weight,Height,type,password")] UserTable userTable)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.UserTables.Add(userTable);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException;
            }
            return View();
        }

//================= UPDATE of CRUD =====================================//
//======================================================================//

        // GET: UserTables/Edit/5
        public ActionResult Edit(int? id)
        {
            UserTable userTable = db.UserTables.Find(id);
            try { 
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                if (userTable == null)
                {
                    return HttpNotFound();
                }
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException;
            }
            return View(userTable);
        }

        // POST: UserTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,First_name,Last_name,Phone,Email,DOB,Sex,Weight,Height,type")] UserTable userTable)
        {
            try
            { 
                if (ModelState.IsValid)
                {
                    db.Entry(userTable).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException;
            }
            return View(userTable);
        }

//================= DELETE of CRUD =====================================//
//======================================================================//

        // GET: UserTables/Delete/5
        public ActionResult Delete(int? id)
        {
            UserTable userTable = db.UserTables.Find(id);
            try
            { 
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if (userTable == null)
                {
                    return HttpNotFound();
                }
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException;
            }
            return View(userTable);
        }

        // POST: UserTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            { 
            UserTable userTable = db.UserTables.Find(id);
            db.UserTables.Remove(userTable);
            db.SaveChanges();
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException;
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    db.Dispose();
                }
                base.Dispose(disposing);
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException;
            }

        }
    }
}
