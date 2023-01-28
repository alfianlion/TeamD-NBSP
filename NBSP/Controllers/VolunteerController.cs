using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NBSP.DAL;
using NBSP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NBSP.Controllers
{
    public class VolunteerController : Controller
    {
        private MemberDAL memberContext = new MemberDAL();
        private VolunteerDAL volunteerContext = new VolunteerDAL();
        private List<string> genderList = new List<string> { "M", "F" };
        private List<string> aList = new List<string> { "Mon", "Tue","Wed", "Thur","Fri", "Sat", "Sun"};
        
        // GET: VolunteerController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Info()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }

        // GET: VolunteerController/Details/5
        public ActionResult Details()
        {
            if ((HttpContext.Session.GetString("Role") == null) ||
  (HttpContext.Session.GetString("Role") != "Volunteer"))
            {
                return RedirectToAction("Index", "Home");
            }
            string name = HttpContext.Session.GetString("LoginID");
            Volunteer volunteer = volunteerContext.GetVolunteerDetail(name);

            return View(volunteer);
        }

        // GET: VolunteerController/Create
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Form()
        {
            ViewData["Gender"] = genderList;
            ViewData["Available"] = aList;
            string name = HttpContext.Session.GetString("LoginID");
            Volunteer volunteer = volunteerContext.GetVolunteerDetail(name);
            HttpContext.Session.SetInt32("VolunteerID", volunteer.VolunteerID);
            return View(volunteer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Form(Volunteer volunteer)
        {
            if (ModelState.IsValid)
            {
                int? id = HttpContext.Session.GetInt32("VolunteerID");
                volunteer = volunteerContext.CheckAvailable(volunteer);
                volunteerContext.UpdateAfter(volunteer, id.Value);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        // POST: VolunteerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VolunteerController/Edit/5
        public ActionResult Edit()
        {
            string name = HttpContext.Session.GetString("LoginID");
            Volunteer volunteer = volunteerContext.GetVolunteerDetail(name);
            HttpContext.Session.SetInt32("VolunteerID", volunteer.VolunteerID);
            return View(volunteer);
        }

        // POST: VolunteerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Volunteer volunteer)
        {
            int id = (int)HttpContext.Session.GetInt32("VolunteerID");
            if (ModelState.IsValid)
            {
                //Update staff record to database
                volunteerContext.Update(volunteer, id);
                HttpContext.Session.SetString("LoginID", volunteer.Name);
                return RedirectToAction("Details");
            }
            else
            {
                //Input validation fails, return to the view
                //to display error message
                return View(volunteer);
            }
        }

        // GET: VolunteerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VolunteerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult EducationPortal()
        {
            return View();
        }
        public ActionResult CounselPortal()
        {
            return View();
        }
        public ActionResult SubsidiesPortal()
        {
            return View();
        }
    }
}
