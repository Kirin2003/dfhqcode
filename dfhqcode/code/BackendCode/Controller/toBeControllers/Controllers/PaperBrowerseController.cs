using Microsoft.AspNetCore.Mvc;

namespace TopConferenceProj.Controllers
{
    public class PaperBrowerseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
