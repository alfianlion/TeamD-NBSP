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
    public class MemberController : Controller
    {
        private MemberDAL memberContext = new MemberDAL();
        private VolunteerDAL volunteerContext = new VolunteerDAL();
        // GET: MemberController
        public ActionResult Index()
        {
            return View();
        }

        // GET: MemberController/Details/5
        public ActionResult Details(int id)
        {
            List<Volunteer> volunteerList = volunteerContext.GetAllVolunteer();
            return View(volunteerList);
        }

        // GET: MemberController/Create
        public ActionResult Create()
        {
            ViewData["ShowResult"] = false;
            Member member = new Member();
            return View(member);
        }

        // POST: SalesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Member member)
        {
            ViewData["ShowResult"] = true;
            if (ModelState.IsValid)
            {
                ViewData["ResultMessage"] = "Customer Created";
                memberContext.Add(member);
                ModelState.Clear();
                return View("Create");
            }
            else
            {
                ViewData["ResultMessage"] = "Customer Already Exists";
                return View();
            }
        }


        // GET: MemberController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MemberController/Edit/5
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

        // GET: MemberController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MemberController/Delete/5
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
