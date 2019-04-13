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
    public class SymptomsController : Controller
    {
        private STContext db = new STContext();

//================= READ of CRUD =======================================//
//======================================================================//


        // GET: Symptoms
        public ActionResult Index()
        {
            var id = Convert.ToInt32(Session["userId"].ToString());         
            var symptoms = db.Symptoms.Where(s => s.User_Id == id);
            return View(symptoms);
        }

        // GET: Symptoms/Details/5
        public ActionResult Details(int? id)
        {
            Symptom symptom = db.Symptoms.Find(id);
            try
            { 
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                if (symptom == null)
                {
                    return HttpNotFound();
                }
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException;
            }
            return View(symptom);
        }

//================= CREATE of CRUD =====================================//
//======================================================================//

        // GET: Symptoms/Create
        public ActionResult Create()
        {
            //var id = Convert.ToInt32(Session["userId"].ToString());
            //var symptoms = db.Symptoms.Where(s => s.User_Id == id);
            try
            {
                ViewBag.User_Id = new SelectList(db.UserTables, "Id", "First_name");
                ViewBag.User_Id = new SelectList(db.UserTables, "Id", "First_name");
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException;
            }
            return View();
        }

        // POST: Symptoms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Symptom_Id,Symptom_Desc,User_Id,C_Time")] Symptom symptom)
        {
            try
            {
                symptom.User_Id = Convert.ToInt32(Session["userId"].ToString());
                if (ModelState.IsValid)
                {
                    db.Symptoms.Add(symptom);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.User_Id = new SelectList(db.UserTables, "Id", "First_name", symptom.User_Id);
                ViewBag.User_Id = new SelectList(db.UserTables, "Id", "First_name", symptom.User_Id);
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException;
            }
            return  RedirectToAction("Index", "Symptoms");
        }


//================= UPDATE of CRUD =====================================//
//======================================================================//

        // GET: Symptoms/Edit/5
        public ActionResult Edit(int? id)
        {
            Symptom symptom = db.Symptoms.Find(id);
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if (symptom == null)
                {
                    return HttpNotFound();
                }
                ViewBag.User_Id = new SelectList(db.UserTables, "Id", "First_name", symptom.User_Id);
                ViewBag.User_Id = new SelectList(db.UserTables, "Id", "First_name", symptom.User_Id);
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException;
            }
          
            return View(symptom);
        }

        // POST: Symptoms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Symptom_Id,Symptom_Desc,User_Id,C_Time")] Symptom symptom)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(symptom).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.User_Id = new SelectList(db.UserTables, "Id", "First_name", symptom.User_Id);
                ViewBag.User_Id = new SelectList(db.UserTables, "Id", "First_name", symptom.User_Id);
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException;
            }
   
            return View(symptom);
        }

//================= DELETE of CRUD =====================================//
//======================================================================//

        // GET: Symptoms/Delete/5
        public ActionResult Delete(int? id)
        {
            Symptom symptom = db.Symptoms.Find(id);
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                if (symptom == null)
                {
                    return HttpNotFound();
                }
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException;
            }

            return View(symptom);
        }

        // POST: Symptoms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Symptom symptom = db.Symptoms.Find(id);

            try
            {
                db.Symptoms.Remove(symptom);
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
