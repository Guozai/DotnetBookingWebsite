using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Asr.Data;
using Asr.Models;

namespace Asr.Controllers
{
    public class HomeController : Controller
    {
        private readonly AsrContext _context;

        // The commented block of code is another way of writing the experession as below
        //public HomeController(AsrContext context)
        //{
        //    _context = context;
        //}
        public HomeController(AsrContext context) => _context = context;

        public IActionResult Index() => View();

        // Why use async 
        // https://stackoverflow.com/questions/14455293/how-and-when-to-use-async-and-await
        public async Task<IActionResult> Slots() => View(await _context.Slot.ToListAsync());
        //public async Task<IActionResult> Index() => View(await _context.Slot.ToListAsync());
            //View(await _context.Slot.Include(s => s.Room).Include(s => s.Staff).Include(s => s.Student).ToListAsync());
        
        public async Task<IActionResult> Rooms() => View(await _context.Room.ToListAsync());

        public async Task<IActionResult> Staffs() => View(await _context.Staff.ToListAsync());

        public IActionResult RoomAvailability() => View();

        [HttpPost]
        [ActionName("RoomAvail")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RoomAvail(DateTime StartTime)
        {
            List<Room> rooms = new List<Room>();
            foreach (var room in _context.Room)
            {
                var count = await _context.Slot.CountAsync(
                    x => x.RoomID == room.RoomID && x.StartTime.Date == StartTime.Date);
                if (count < 2)
                    rooms.Add(room);
            }
            //RedirectToAction(nameof(Rooms));

            return View(rooms);
        }

        public async Task<IActionResult> Students() => View(await _context.Student.ToListAsync());

        public IActionResult StaffAvailability() => View();

        [HttpPost]
        [ActionName("StaffAvail")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StaffAvail(DateTime StartTime)
        {
            List<Staff> staffs = new List<Staff>();
            foreach (var staff in _context.Staff)
            {
                var count = await _context.Slot.CountAsync(
                    x => x.StaffID == staff.StaffID && x.StartTime.Date == StartTime.Date);
                if (count < 4)
                    staffs.Add(staff);
            }

            return View(staffs);
        }

        public IActionResult FAQ()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Sitemap() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() =>
            View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
