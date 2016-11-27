using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EventeterAppAlpha.Models;

namespace EventeterAppAlpha.Controllers
{
    public class UserdatasController : Controller
    {
        private EventsterDBEntities db = new EventsterDBEntities();

        // GET: Userdatas
        public ActionResult Index()
        {
            return View(db.Userdatas.ToList());
        }

        // GET: Userdatas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Userdata userdata = db.Userdatas.Find(id);
            if (userdata == null)
            {
                return HttpNotFound();
            }
            return View(userdata);
        }

        // GET: Userdatas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Userdatas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,Username,Password,Email")] Userdata userdata)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (db)
                    {
                        //Checks for existing user email in the Userdata table
                        List<Userdata> obj = db.Userdatas.Where(u => u.Email == userdata.Email).ToList();
                        if (obj.Count > 0)
                        {

                            Response.Write("<script> alert('User already exists, Please enter a valid password')</script>");
                            //Session["Email"] = uData.Email;
                            //return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            db.Userdatas.Add(userdata);
                            db.SaveChanges();
                            return RedirectToAction("Login", "Home");
                        }

                    }

                }

                return View(userdata);
            }
            catch (Exception)
            {

                return RedirectToAction("Contact", "Home");
            }
        }

   
        // GET: Userdatas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Userdata userdata = db.Userdatas.Find(id);
            if (userdata == null)
            {
                return HttpNotFound();
            }
            return View(userdata);
        }

        // POST: Userdatas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,Username,Password,Email")] Userdata userdata)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userdata).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userdata);
        }

        // GET: Userdatas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Userdata userdata = db.Userdatas.Find(id);
            if (userdata == null)
            {
                return HttpNotFound();
            }
            return View(userdata);
        }

        // POST: Userdatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Userdata userdata = db.Userdatas.Find(id);
            db.Userdatas.Remove(userdata);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
