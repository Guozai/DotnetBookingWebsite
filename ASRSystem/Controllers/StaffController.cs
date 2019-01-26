using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Asr.Data;

namespace ASRSystem.Controllers
{
    [Authorize(Roles = Constants.StaffRole)]
    public class StaffController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}