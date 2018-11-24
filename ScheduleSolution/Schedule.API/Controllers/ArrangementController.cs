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

            ArrangementSearchContext searchContext = null;
            if (ModelState.IsValid)
            {
                searchContext = new ArrangementSearchContext()
                {
                    Complexity = arrangementSearchBag?.Complexity,
                    DateTime = arrangementSearchBag?.DateTime,
                    IsHandled = arrangementSearchBag?.IsHandled,
                    OrderByCriterion = arrangementSearchBag?.OrderExpression,
                    PersonId = arrangementSearchBag?.PersonId,
                    SubjectId = arrangementSearchBag?.SubjectId,
                    Tags = arrangementSearchBag?.Tags
                };
            }

            var model = new ArrangementListViewModel()
            {
                SearchBag = new ArrangementSearchBag()
                {
                    Subjects = (await _subjectService.GetAsync()).Select(e => new SubjectDto()
                        {Id = e.Id, Label = e.Name}),
                    People = (await _personService.GetAsync()).Select(e => new PersonDto()
                        {Id = e.Id, ShortDescription = $"{e.Name} {e.Surname}, {e.AlternativeEgo}"})
                },
                ViewArrangementDtos = await _arrangementService.GetAsync(searchContext)
            };
            
            return View(model);
        }
    

        public async Task<ActionResult> Edit(int id)
        {
            return View("Create", await _arrangementService.GetAsync(id));
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
                if (ModelState.IsValid)
                {
                    await _arrangementService.CreateOrUpdateAsync(arrangementDto);

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    var emptyArrangement = _arrangementService.Empty();
                    arrangementDto.AvailableTags = emptyArrangement.AvailableTags;
                    arrangementDto.AvailablePeople = emptyArrangement.AvailablePeople;
                    arrangementDto.AvailableSubjects = emptyArrangement.AvailableSubjects;

                    return View(arrangementDto);
                }
            }
            catch
            {
                return View();
            }
        }
        
    }
}