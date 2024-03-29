using Microsoft.AspNetCore.Mvc;

namespace Crochet.Controllers
{
    public class StitchesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ChainStitch()
        {
            return View();
        }

        public IActionResult SingleStitch()
        {
            return View();
        }

        public IActionResult HalfDoubleStitch()
        {
            return View();
        }

        public IActionResult DoubleStitch()
        {
            return View();
        }

        public IActionResult HalfTripleStitch()
        {
            return View();
        }

        public IActionResult TripleStitch()
        {
            return View();
        }
    }
}