using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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
            ViewBag.Message = "Oh snap! Looks like an error occured...";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Oh snap! Looks like an error occured...";

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

        //Inner Join - Returns a minimum of one match between Sport & Location
        public ActionResult SportLocation()
        {
            return View(db.GetInnerJoinSport_and_Location());
        }
        //Inner Join - Returns a minimum of one match between Event & Location
        public ActionResult EventLocation()
        {
            return View(db.GetInnerJoinEvent_and_Location());
        }
        //Inner Join - Returns a minimum of one match between Event & User
        public ActionResult EventUser()
        {
            return View(db.GetInnerJoinEvent_and_User());
        }
        //Right Join - Returns all rows from right table and matched rows from left table. Includes DISTINCT to negate NULL values
        public ActionResult TeamEvent()
        {
            return View(db.GetRightOuterJoinTeam_Event());
        }

        //Left join - Returns all rows from the left table and matched rows from the right table. Includes DISTINCT to negate NULL values
        public ActionResult SportTeam()
        {
            return View(db.GetInnerJoinSport_Team());
        }

        //Left join - Returns all rows from the left table and matched rows from the right table. Includes DISTINCT to negate NULL values
        public ActionResult UserTeam()
        {
            return View(db.GetLeftOuterJoinUser_Team());
        }

        //List of events
        public ActionResult EventList()
        {
            return View(db.GetEventsList());
        }
        //List of users
        public ActionResult UserList()
        {
            return View(db.GetUserList());
        }

        //List of teams
        public ActionResult TeamsList()
        {
            return View(db.GetTeamList());
        }

        //List of locations
        public ActionResult LocationList()
        {
            return View(db.GetLocationList());
        }


        //List of sports
        public ActionResult SportList()
        {
            return View(db.GetSportsList());
        }
    }
}

