// Copy from MvcMovie example of Week7
// And https://stackoverflow.com/questions/32138022/how-to-map-composite-key-in-crud-functionality

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        //public async Task<IActionResult> Create()
        //{
        //    var rooms = _context.Room.Select(x => x.RoomID).Distinct().OrderBy(x => x);

        //    return View(new StaffCreateViewModel
        //    {
        //        RoomIDs = new SelectList(await rooms.ToListAsync())
        //    });
        //}
        public IActionResult Create() => View();

        // POST: Slots/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string RoomID, DateTime StartTime)
        {
            Slot slot = new Slot();

            if (ModelState.IsValid)
            {
                var StaffID = GetUserID(User.Identity.Name);

                // count the number of slots this staff created on this day
                var countStaff = await _context.Slot.CountAsync(
                    x => x.StaffID == StaffID && x.StartTime.Date == StartTime.Date);
                // count the number of times this room is being used
                var countRoom = await _context.Slot.CountAsync(
                    x => x.RoomID == RoomID && x.StartTime.Date == StartTime.Date);

                // If slot is not created in the past
                // and staff has not created more than 4 slots in this day
                // and room has not been used for more than twice in this day
                if (StartTime >= DateTime.Now
                    && countStaff < 4 && countRoom < 2 
                    && StartTime.Hour >= 9 && StartTime.Hour <= 14)
                {
                    slot.RoomID = RoomID;
                    var time = StartTime.Hour + ":00";
                    slot.StartTime = DateTime.ParseExact(
                        StartTime.Date.ToString("yyyy/MM/dd") + " " + time, "yyyy/MM/dd HH:mm", null);
                    slot.StaffID = StaffID;
                    slot.StudentID = null;

                    _context.Add(slot);
                }
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            //ViewBag.Rooms = new SelectList();

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

            if (slot.StudentID == null)
            {
                _context.Slot.Remove(slot);
            }
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
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