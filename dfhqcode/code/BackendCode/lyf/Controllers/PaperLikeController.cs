using Microsoft.AspNetCore.Mvc;

namespace TopConferenceProj.Controllers
{
    public class PaperLikeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
