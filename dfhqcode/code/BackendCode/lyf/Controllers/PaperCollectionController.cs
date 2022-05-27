using Microsoft.AspNetCore.Mvc;

namespace TopConferenceProj.Controllers
{
    public class PaperCollectionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
