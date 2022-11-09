using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public class SingleParent : Controller
    {
        // GET: SingleParent
        public ActionResult Index()
        {
            return View();
        }

        // GET: SingleParent/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SingleParent/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SingleParent/Create
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

        // GET: SingleParent/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SingleParent/Edit/5
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

        // GET: SingleParent/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SingleParent/Delete/5
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
