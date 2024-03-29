using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Crochet.Models;
using Crochet.Data;
using Crochet.Data.Repositories;

namespace Crochet.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentsRepository crepository;
        private readonly IPatternsRepository prepository;
        private readonly AppDbContext context;
        private readonly UserManager<AppUserModel> userManager;

        public CommentsController(ICommentsRepository c, IPatternsRepository p, AppDbContext dbcontext, UserManager<AppUserModel> u)
        {
            crepository = c;
            prepository = p;
            context = dbcontext;
            userManager = u;
        }

        [HttpGet]
        public IActionResult AddComment(int patternId)
        {
            var pattern = prepository.GetPatternsByIdAsync(patternId).Result;
            var comment = new CommentModel { Pattern = pattern, PatternId = patternId };
            return View(comment);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(CommentModel model)
        {
            if (userManager != null)
            {
                model.User = userManager.GetUserAsync(User).Result;
            }

            await crepository.StoreCommentsAsync(model);
            return RedirectToAction("Index", "Patterns");
        }
    }
}