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
        private VolunteerDAL volunteerContext = new VolunteerDAL();
        // GET: VolunteerController
        public ActionResult Index()
        {
            return View();
        }

        // GET: VolunteerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: VolunteerController/Create
        public ActionResult Create()
        {
            ViewData["ShowResult"] = false;
            Volunteer volunteer = new Volunteer();
            return View(volunteer);
        }

        // POST: SalesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Volunteer volunteer)
        {
            ViewData["ShowResult"] = true;
            if (ModelState.IsValid)
            {
                ViewData["ResultMessage"] = "Volunteer Created";
                volunteerContext.Add(volunteer);
                ModelState.Clear();
                return View("Create");
            }
            else
            {
                ViewData["ResultMessage"] = "Volunteer Already Exists";
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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: VolunteerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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
    }
}
