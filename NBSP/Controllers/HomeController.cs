using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NBSP.DAL;
using NBSP.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NBSP.Controllers
{
    public class HomeController : Controller
    {
        private MemberDAL memberContext = new MemberDAL();
        private VolunteerDAL volunteerContext = new VolunteerDAL();
        private SqlConnection conn;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Profile()
        {
            return View();
        }
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(IFormCollection formData)
        {

            // Read inputs from textboxes
            // Email address converted to lowercase
            string loginID = formData["txtLoginID"];
            string password = formData["txtPassword"].ToString();
            bool memberLoginAuth = memberContext.MemberLoginCheck(loginID, password);
            bool volunteerLoginAuth = volunteerContext.LoginCheck(loginID, password);

            if (memberLoginAuth)
            {
                // Store Login ID in session with the key “LoginID”
                HttpContext.Session.SetString("LoginID", loginID);
                // Store user role “SalesPersonnel” as a string in session with the key “Role”
                HttpContext.Session.SetString("Role", "Member");

                // Redirect user to the "SalesPersonnelMain" view through an action
                return RedirectToAction("Index", "Member");
            }
            if (volunteerLoginAuth){
                // Store Login ID in session with the key “LoginID”
                HttpContext.Session.SetString("LoginID", loginID);
                // Store user role “SalesPersonnel” as a string in session with the key “Role”
                HttpContext.Session.SetString("Role", "Member");

                // Redirect user to the "SalesPersonnelMain" view through an action
                return RedirectToAction("Index", "Volunteer");
            }
            else
            {
                TempData["Message"] = "You are wrong";
                return RedirectToAction("LogIn");
            }
        }

        public IActionResult SignUp()
        {
            return View();
        }
        public IActionResult SignUpMember()
        {
            ViewData["ShowResult"] = false;
            Member member = new Member();
            return View();
        }

        public IActionResult SignUpVolunteer()
        {
            ViewData["ShowResult"] = false;
            Volunteer member = new Volunteer();
            return View();
        }

        [HttpPost]
        public IActionResult SignUpMember(Member member)
        {
            ViewData["ShowResult"] = true;
            if (ModelState.IsValid)
            {
                ViewData["ResultMessage"] = "Customer Created";
                memberContext.Add(member);
                ModelState.Clear();
                return View("LogIn");
            }
            else
            {
                ViewData["ResultMessage"] = "Customer Already Exists";
                return View();
            }
        }

        [HttpPost]
        public IActionResult SignUpVolunteer(Volunteer volunteer)
        {
            ViewData["ShowResult"] = true;
            if (ModelState.IsValid)
            {
                ViewData["ResultMessage"] = "Customer Created";
                volunteerContext.Add(volunteer);
                ModelState.Clear();
                return View("LogIn");
            }
            else
            {
                ViewData["ResultMessage"] = "Customer Already Exists";
                return View();
            }
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
