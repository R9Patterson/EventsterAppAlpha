﻿using System;
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
    public class EventsController : Controller
    {
        private EventsterDBEntities db = new EventsterDBEntities();

        // GET: Events
        public ActionResult Index()
        {
            var events = db.Events.Include(e => e.Team).Include(e => e.Userdata);
            return View(events.ToList());
        }

        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            ViewBag.TeamID = new SelectList(db.Teams, "TeamID", "TeamName");
            ViewBag.UserID = new SelectList(db.Userdatas, "UserID", "Username");
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "EventID,UserID,EventTitle,HostName,EventDate,PhoneNumber,Description,TeamID,Email")] Event
                @event)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    db.Events.Add(@event);
                    db.SaveChanges();
                    return RedirectToAction("Create", "Locations");
                }

                ViewBag.TeamID = new SelectList(db.Teams, "TeamID", "TeamName", @event.TeamID);
                ViewBag.UserID = new SelectList(db.Userdatas, "UserID", "Username", @event.UserID);
                return View(@event);
            }
            catch (Exception)
            {
                return RedirectToAction("About", "Home");
            }
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            ViewBag.TeamID = new SelectList(db.Teams, "TeamID", "TeamName", @event.TeamID);
            ViewBag.UserID = new SelectList(db.Userdatas, "UserID", "Username", @event.UserID);
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventID,UserID,EventTitle,HostName,EventDate,PhoneNumber,Description,TeamID,Email")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
                
               
            }
            ViewBag.TeamID = new SelectList(db.Teams, "TeamID", "TeamName", @event.TeamID);
            ViewBag.UserID = new SelectList(db.Userdatas, "UserID", "Username", @event.UserID);
            return View(@event);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
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
