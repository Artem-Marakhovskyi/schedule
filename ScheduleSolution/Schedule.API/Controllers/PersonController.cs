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
    public class PersonController : Controller
    {
        private PersonService _service;

        public PersonController(
            PersonService service)
        {
            _service = service;
        }

        // GET: Person
        public async Task<ActionResult> Index()
        {
            var people = await _service.GetAsync();
            return View(people);
        }
        
        // GET: Person/CreateAsync
        public ActionResult Create()
        {
            return View();
        }

        // POST: Person/CreateAsync
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Person person)
        {
            if (string.IsNullOrWhiteSpace(person.AlternativeEgo))
            {
                ModelState.AddModelError(nameof(person.AlternativeEgo), "Alternative ego is empty");
            }
            if (string.IsNullOrWhiteSpace(person.Name))
            {
                ModelState.AddModelError(nameof(person.Name), "Name is empty");
            }
            if (string.IsNullOrWhiteSpace(person.Surname))
            {
                ModelState.AddModelError(nameof(person.Surname), "Surname is empty");
            }

            if (ModelState.IsValid)
            {
                await _service.SaveAsync(person);
                return Redirect(nameof(Index));
            }
            else
            {
                return View(person);
            }
        }
        
    }
}