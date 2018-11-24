using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Schedule.BLL;
using Schedule.BLL.DTO;

namespace Schedule.API.Controllers
{
    public class TagController : Controller
    {
        private readonly TagService _service;

        public TagController(TagService service)
        {
            _service = service;
        }

        // GET: Tag
        public async Task<ActionResult> Index()
        {
            var tags = await _service.GetAsync();
            return View(tags);
        }
        
        // GET: Tag/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tag/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TagDto tagDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _service.CreateAsync(tagDto);

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(tagDto);
                }
            }
            catch
            {
                return View();
            }
        }
    }
}