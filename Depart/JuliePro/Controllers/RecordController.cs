using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JuliePro.Services;
using JuliePro.Models;

namespace JuliePro.Controllers
{
    public class RecordController : Controller
    {
        private readonly IRecordService _service;

        public RecordController(IRecordService service)
        {
            _service = service;
        }

        // GET: Record
        public async Task<IActionResult> Index()
        {

            return View(await _service.GetAllAsync());
        }


        public async Task<IActionResult> TrainerIndex(int trainerId)
        {

            return View(await _service.GetAllByTrainerIdAsync(trainerId));
        }

        // GET: Record/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var xrecord = await _service.GetByIdAsync(id.Value);

            if (xrecord == null)
            {
                return NotFound();
            }

            return View(xrecord);
        }

        // GET: Record/Create
        public async Task<IActionResult> Create()
        {

            var model = new RecordViewModel() { Date = DateTime.Now };
            await _service.PopulateTrainersDisciplinesAsync(model);

            return View(model);
        }

        // POST: Record/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RecordViewModel xrecord)
        {

            ModelState.Remove(nameof(xrecord.AvailableOptions));

            if (ModelState.IsValid)
            {
                await _service.CreateAsync(xrecord);
                return RedirectToAction(nameof(Index));
            }

            await _service.PopulateTrainersDisciplinesAsync(xrecord);
            return View(xrecord);
        }

        // GET: Record/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var xrecord = await _service.GetByIdAsync(id.Value);
            if (xrecord == null)
            {
                return NotFound();
            }

            return View(xrecord);
        }

        // POST: Record/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //  [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RecordViewModel xrecord)
        {
            ModelState.Remove(nameof(xrecord.AvailableOptions));

            if (id != xrecord.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.EditAsync(xrecord);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _service.ExistsAsync(xrecord.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            await _service.PopulateTrainersDisciplinesAsync(xrecord);

            return View(xrecord);
        }

        // GET: Record/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var xrecord = await _service.GetByIdAsync(id.Value);
            if (xrecord == null)
            {
                return NotFound();
            }

            return View(xrecord);
        }

        // POST: Record/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
