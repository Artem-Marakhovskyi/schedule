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
        

        
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Subject subject)
        {
            if (string.IsNullOrWhiteSpace(subject.Name))
            {
                ModelState.AddModelError(nameof(subject.Name), "Name is empty");
            }

            if (string.IsNullOrWhiteSpace(subject.Description))
            {
                ModelState.AddModelError(nameof(subject.Description), "Description is empty");
            }

            if (ModelState.IsValid)
            {
                await _service.CreateAsync(subject);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(subject);
            }

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