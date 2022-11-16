using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NBSP.DAL;
using NBSP.Models;
using System;
using System.Collections;
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
            List<Volunteer> volunteerList = volunteerContext.GetAllVolunteer();
            return View(volunteerList);
        }
        public IActionResult Details()
        {
            if ((HttpContext.Session.GetString("Role") == null) ||
                (HttpContext.Session.GetString("Role") != "Member"))
            {
                return RedirectToAction("Index", "Home");
            }
            string name = HttpContext.Session.GetString("LoginID");
            Member member = memberContext.GetMemberDetail(name);

            return View(member);
        }

        // GET: MemberController/=ViewVolunteerList
        public ActionResult ViewVolunteer()
        {
            List<Volunteer> volunteerList = volunteerContext.GetAllVolunteer();
            return View(volunteerList);
        }

        // GET: MemberController/Edit/5
        public ActionResult Edit()
        {
            string name = HttpContext.Session.GetString("LoginID");
            Member member = memberContext.GetMemberDetail(name);
            HttpContext.Session.SetInt32("MemberID", member.MemberID);
            return View(member);
        }

        // POST: MemberController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Member member)
        {
            int id= (int)HttpContext.Session.GetInt32("MemberID");
            if (ModelState.IsValid)
            {
                //Update staff record to database
                memberContext.Update(member,id);
                HttpContext.Session.SetString("LoginID", member.Name);
                return RedirectToAction("Details");
            }
            else
            {
                //Input validation fails, return to the view
                //to display error message
                return View(member);
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
