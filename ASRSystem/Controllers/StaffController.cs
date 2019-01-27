// Copy from MvcMovie example of Week7
// And https://stackoverflow.com/questions/32138022/how-to-map-composite-key-in-crud-functionality

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Asr.Data;
using Asr.Models;

namespace ASRSystem.Controllers
{
    [Authorize(Roles = Constants.StaffRole)]
    public class StaffController : Controller
    {
        private readonly AsrContext _context;

        public StaffController(AsrContext context) => _context = context;

        // GET: Slots
        public async Task<IActionResult> Index() => View(await _context.Slot.ToListAsync());

        // GET: Slots/Create
        public IActionResult Create() => View();

        // POST: Slots/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoomID,StartTime,StaffID,StudentID")] Slot slot)
        {
            if (ModelState.IsValid)
            {
                _context.Add(slot);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(slot);
        }

        // GET: Slots/Delete
        public async Task<IActionResult> Delete(string RoomID, DateTime? StartTime)
        {
            if (StartTime == null)
                return NotFound();

            var slot = await _context.Slot.FirstOrDefaultAsync(x => x.RoomID == RoomID && x.StartTime == StartTime);
            if (slot == null)
                return NotFound();

            return View(slot);
        }

        // POST: Slots/Delete
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string RoomID, DateTime StartTime)
        {
            var slot = await _context.Slot.FirstOrDefaultAsync(x => x.RoomID == RoomID && x.StartTime == StartTime);
            _context.Slot.Remove(slot);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}