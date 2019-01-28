using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Asr.Data;

namespace ASRSystem.Controllers
{
    [Authorize(Roles = Constants.StudentRole)]
    public class StudentController : Controller
    {
        private readonly AsrContext _context;

        public StudentController(AsrContext context) => _context = context;

        // GET: Slots
        public async Task<IActionResult> Index() => View(await _context.Slot.ToListAsync());

        // GET: Movies/Edit
        public async Task<IActionResult> Edit(string RoomID, DateTime? StartTime)
        {
            if (StartTime == null)
                return NotFound();

            var slot = await _context.Slot.FirstOrDefaultAsync(x => x.RoomID == RoomID && x.StartTime == StartTime);
            if (slot == null)
                return NotFound();

            return View(slot);
        }

        // POST: Movies/Edit
        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string RoomID, DateTime StartTime)
        {
            if (StartTime == null)
                return NotFound();

            var slot = await _context.Slot.FirstOrDefaultAsync(x => x.RoomID == RoomID && x.StartTime == StartTime);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(slot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (slot == null)
                        return NotFound();
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(slot);
        }
    }
}