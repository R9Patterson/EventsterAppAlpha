using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventeterAppAlpha.Models;

namespace EventeterAppAlpha.Controllers
{
    public class HomeController : Controller
    {
        private EventsterDBEntities db = new EventsterDBEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(login uData)
        {
            if (ModelState.IsValid)
            {
                using (db)
                {
                    Userdata obj = db.Userdatas.FirstOrDefault(u => u.Email.Equals(uData.Email) && u.Password.Equals(uData.Password));
                    if (obj != null)
                    {
                        
                        if (uData.Email == "adminrp@admin.com")
                        {
                            
                            return RedirectToAction("Index", "Events");
                        }
                        else
                        {
                            Session["Email"] = uData.Email;
                            return RedirectToAction("Index", "Home");
                        }
                        
                    }
                    else
                    {
                        Response.Write("<script> alert('Please enter a valid password')</script>");
                    }

                }
                
            }
            return View();
        }

        public ActionResult Logoff()
        {
           Session.Clear();
            return RedirectToAction("Login", "Home");
        }


        //Total number of events in database
        public ActionResult EventCount()
        {
            return View(db.GetCountOfEvents());
        }
        //Total number of users in the database
        public ActionResult CountUser()
        {
            return View(db.GetCountUsers());
        }

        //Total number of teams in the database
        public ActionResult TeamsCount()
        {
            return View(db.GetCountTeams());
        }

        //Total number of locations in the database
        public ActionResult LocationCount()
        {
            return View(db.GetCountDistinctLocations());
        }


        //Total number of sports in the database
        public ActionResult SportCount()
        {
            return View(db.GetDistinctCount());
        }
    }
}