using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Schedule.API.Models;
using Schedule.BLL;
using Schedule.BLL.DTO;

namespace Schedule.API.Controllers
{
    public class ArrangementController : Controller
    {
        private readonly PersonService _personService;
        private readonly SubjectService _subjectService;
        private readonly ArrangementService _arrangementService;

        public ArrangementController(
            ArrangementService arrangementService,
            SubjectService subjectService,
            PersonService personService)
        {
            _personService = personService;
            _subjectService = subjectService;
            _arrangementService = arrangementService;
        }

        // GET: Arrangement
        public async Task<ActionResult> Index(ArrangementListViewModel arrangementListViewModel = null)
        {
            var arrangementSearchBag = arrangementListViewModel?.SearchBag;

            return View(
                new ArrangementListViewModel()
                {
                    SearchBag = new ArrangementSearchBag()
                    {
                        Subjects = (await _subjectService.GetAsync()).Select(e => new SubjectDto() { Id = e.Id, Label = e.Name}),
                        People = (await _personService.GetAsync()).Select(e => new PersonDto() { Id = e.Id, ShortDescription = $"{e.Name} {e.Surname}, {e.AlternativeEgo}"})
                    },
                    ViewArrangementDtos = await _arrangementService.GetAsync(
                        new ArrangementSearchContext()
                        {
                            Complexity = arrangementSearchBag?.Complexity,
                            DateTime = arrangementSearchBag?.DateTime,
                            IsHandled = arrangementSearchBag?.IsHandled,
                            OrderByCriterion = arrangementSearchBag?.OrderExpression,
                            PersonId = arrangementSearchBag?.PersonId,
                            SubjectId = arrangementSearchBag?.SubjectId,
                            Tags = arrangementSearchBag?.Tags
                        })
                });
        }
        
        // GET: Arrangement/Create
        public ActionResult Create()
        {
            var arrangement = _arrangementService.Empty();
            return View(arrangement);
        }

        public ActionResult Handle(int id)
        {
            if (id != 0)
                _arrangementService.Handle(id);

            return RedirectToAction("Index", "Arrangement");

        }

        // POST: Arrangement/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ArrangementDto arrangementDto)
        {
            try
            {
                await _arrangementService.CreateOrUpdateAsync(arrangementDto);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Arrangement/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Arrangement/Edit/5
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

        // GET: Arrangement/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Arrangement/Delete/5
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