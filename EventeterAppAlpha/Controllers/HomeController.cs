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
                        Session["Email"] = uData.Email;
                        return RedirectToAction("Index", "Home");
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
    }
}