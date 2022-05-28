using Microsoft.AspNetCore.Mvc;

namespace TopConferenceProj.Controllers
{
    public class PaperSearchController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
