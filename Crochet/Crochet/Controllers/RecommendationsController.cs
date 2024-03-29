using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Crochet.Models;
using Crochet.Data;
using Crochet.Data.Repositories;

namespace Crochet.Controllers
{
    public class RecommendationsController : Controller
    {
        private readonly AppDbContext context;
        private readonly IRecommendationsRepository rrepository;
        private readonly UserManager<AppUserModel> userManager;

        public RecommendationsController(AppDbContext c, IRecommendationsRepository r, UserManager<AppUserModel> u)
        {
            context = c;
            rrepository = r;
            userManager = u;
        }

        public IActionResult Index()
        {
            var recommends = rrepository.GetRecommendations();
            return View(recommends);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("Edit", new RecommendationModel());
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var recommend = rrepository.GetRecommendationsByIdAsync(id).Result;
            return View(recommend);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RecommendationModel recommend)
        {
            if (!ModelState.IsValid)
                return View(recommend);

            if (recommend.RecommendId == 0)
                await rrepository.StoreRecommendationsAsync(recommend);
            else
                rrepository.UpdateRecommendations(recommend);

            return RedirectToAction("Index", "Recommendations");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(int recommendId)
        {
            var recommend = await rrepository.GetRecommendationsByIdAsync(recommendId);
            return View(recommend);
        }

        [HttpPost]
        public IActionResult Delete(RecommendationModel model)
        {
            rrepository.DeleteRecommendations(model.RecommendId);
            return RedirectToAction("Index", "Recommendations");
        }
    }
}