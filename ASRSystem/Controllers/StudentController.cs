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
                    // Get current user's userID
                    var userID = GetUserID(User.Identity.Name);

                    var slotBooked = await _context.Slot.FirstOrDefaultAsync(
                        x => x.StudentID == userID && x.StartTime.Date == StartTime.Date);

                    if (userID.StartsWith('s'))
                    {
                        if (slot.StudentID == null && slotBooked == null)
                            slot.StudentID = userID;
                        if (slot.StudentID != null && slot.StudentID == userID)
                            slot.StudentID = null;
                    }

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

        private string GetUserID(string userEmail)
        {
            // Separate the input string by '@'
            char[] seps = { '@' };
            string[] parts = userEmail.Split(seps);

            if (parts.Length == 2)
                return parts[0];

            return "";
        }
    }
}