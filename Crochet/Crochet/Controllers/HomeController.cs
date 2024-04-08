using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Diagnostics;
using Crochet.Data;
using Crochet.Data.Repositories;
using Crochet.Models;

namespace Crochet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext context;
        private readonly IPhotosRepository prepository;

        public HomeController(ILogger<HomeController> logger, AppDbContext c, IPhotosRepository p)
        {
            _logger = logger;
            context = c;
            prepository = p;
        }

        public IActionResult Index()
        {
            var photos = prepository.GetPhotos();
            return View(photos);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}