using System.Diagnostics;
using System.Threading.Tasks;
using Asr.Data;
using Microsoft.AspNetCore.Mvc;
using Asr.Models;
using Microsoft.EntityFrameworkCore;

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

        //public IActionResult Index() => View();

        // Why use async 
        // https://stackoverflow.com/questions/14455293/how-and-when-to-use-async-and-await
        //public async Task<IActionResult> Slots() => View(await _context.Slot.ToListAsync());
        public async Task<IActionResult> Index() => View(await _context.Slot.ToListAsync());
            //View(await _context.Slot.Include(s => s.Room).Include(s => s.Staff).Include(s => s.Student).ToListAsync());
        
        public async Task<IActionResult> Rooms() => View(await _context.Room.ToListAsync());

        //public IActionResult About()
        //{
        //    ViewData["Message"] = "Your application description page.";

        //    return View();
        //}

        //public IActionResult Contact()
        //{
        //    ViewData["Message"] = "Your contact page.";

        //    return View();
        //}

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() =>
            View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
