using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Schedule.BLL;
using Schedule.Data;

namespace Schedule.API.Controllers
{
    public class SubjectController : Controller
    {
        private readonly SubjectService _service;

        public SubjectController(
            SubjectService service)
        {
            _service = service;
        }

        // GET: Subject
        public async Task<ActionResult> Index()
        {
            var subjects = await _service.GetAsync();
            return View(subjects);
        }

        // GET: Subject/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Subject/CreateAsync
        public ActionResult Create()
        {
            return View();
        }

        // POST: Subject/CreateAsync
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Subject subject)
        {
            await _service.CreateAsync(subject);
            return RedirectToAction(nameof(Index));
        }

        // GET: Subject/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Subject/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Subject/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Subject/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}