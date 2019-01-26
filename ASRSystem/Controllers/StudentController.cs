using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Asr.Data;

namespace ASRSystem.Controllers
{
    [Authorize(Roles = Constants.StudentRole)]
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}