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
    public class AdminController : Controller
    {
        private VolunteerDAL volunteerContext = new VolunteerDAL();
        private JobDAL jobContext = new JobDAL();
        private DonationDAL donationContext = new DonationDAL();
        // GET: AdminController
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
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
        public ActionResult Job()
        {
            return View();
        }
        public ActionResult ViewVolunteer()
        {
            List<Volunteer> volunteerList = volunteerContext.GetAllVolunteer();
            return View(volunteerList);
        }
        public ActionResult ViewDonation()
        {
            List<Donation> donationList = donationContext.GetAllDonation();
            return View(donationList);
        }

        // GET: AdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminController/Create
        public ActionResult CreateJob()
        {
            Job job = new Job();
            return View();
        }
        public ActionResult ViewJob()
        {
            List<Job> jobList = jobContext.GetAllJob();
            return View(jobList);

        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateJob(Job job)
        {
            jobContext.Add(job);
            return View("Job");
        }
        public ActionResult DeleteJob(int id)
        {
            Job job = jobContext.GetDetail(id);
            return View(job);
        }
        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteJob(Job job)
        {
            // Delete the staff record from database
            jobContext.Delete(job.JobID);
            return RedirectToAction("ViewJob");
        }
        public ActionResult JobPortal()
        {
            List<Job> jobList = jobContext.GetAllJob();
            return View(jobList);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult JobPortal(IFormCollection formData)
        {
            ViewData["ShowResult"] = true;
            List<Job> jobList = jobContext.Search(formData["searchInput"].ToString());
            if (jobList.Count > 0)
            {
                ViewData["ResultMessage"] = "";
            }
            else
            {
                ViewData["ResultMessage"] = "Job Does Not Exist";
            }
            return View(jobList);
        }
        public ActionResult ViewJobDetails(int id)
        {
            Job job = jobContext.GetDetail(id);
            //Customer customerCheck = CustomerDAL.GetDetails(memberID, password)
            return View(job);
        }

        // GET: AdminController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminController/Edit/5
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

        // GET: AdminController/Delete/5
        public ActionResult Delete(string id)
        {
            Volunteer volunteer = volunteerContext.GetVolunteerDetail(id);
            return View(volunteer);
        }
        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Volunteer volunteer)
        {
            // Delete the staff record from database
            volunteerContext.Delete(volunteer.Name);
            return RedirectToAction("ViewVolunteer");
        }

    }
}
