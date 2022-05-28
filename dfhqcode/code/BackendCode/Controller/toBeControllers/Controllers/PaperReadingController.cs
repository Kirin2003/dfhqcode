using Microsoft.AspNetCore.Mvc;

namespace TopConferenceProj.Controllers
{
    public class PaperReadingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
