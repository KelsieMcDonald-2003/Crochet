using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Crochet.Models;
using Crochet.Data;
using Crochet.Data.Repositories;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.FileSystemGlobbing.Internal;

namespace Crochet.Controllers
{
    public class PatternsController : Controller
    {
        private readonly IPatternsRepository prepository;
        private readonly ICommentsRepository crepository;
        private readonly AppDbContext context;
        private readonly UserManager<AppUserModel> userManager;

        public PatternsController(IPatternsRepository p, ICommentsRepository c, AppDbContext dbcontext, UserManager<AppUserModel> u)
        {
            prepository = p;
            crepository = c;
            context = dbcontext;
            userManager = u;
        }


        public IActionResult Index()
        {
            var patterns = prepository.GetPatterns();
            return View(patterns);
        }

        /*
        [HttpPost]
        public IActionResult Index(string user)
        {
            var patterns = prepository.GetPatterns()
                .Where(p => p.User.UserName == user)
                .ToList();

            return View("Index", patterns);
        }
        

        [HttpPost]
        public IActionResult Index(string searchString, string user)
        {
            var patterns = prepository.GetPatterns()
                .Where(p => p.User.UserName == user)
                .Where(p => p.PatternTitle.Contains(searchString))
                .ToList();

            return View("Index", patterns);
        }
        */

        [HttpPost]
        public IActionResult Index(string searchString, string user)
        {
            IEnumerable<PatternModel> patterns = prepository.GetPatterns();

            if (!string.IsNullOrEmpty(user))
            {
                patterns = patterns.Where(p => p.User.UserName == user);
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                patterns = patterns.Where(p => p.PatternTitle.Contains(searchString));
            }

            return View("Index", patterns.ToList());
        }


        [HttpGet]
        [Authorize]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Level = context.Levels.OrderBy(l => l.LevelId).ToList();
            ViewBag.Hook = context.Hooks.OrderBy(h => h.HookId).ToList();
            return View("Edit", new PatternModel());
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Level = context.Levels.OrderBy(l => l.LevelId).ToList();
            ViewBag.Hook = context.Hooks.OrderBy(h => h.HookId).ToList();
            var pattern = prepository.GetPatternsByIdAsync(id).Result;
            return View(pattern);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(PatternModel model)
        {
            if (ModelState.IsValid)
            {
                model.Date = DateTime.Now;
                if (userManager != null)
                {
                    model.User = userManager.GetUserAsync(User).Result;
                }

                if (model.PatternId == 0)
                    await prepository.StorePatternsAsync(model);
                else
                    prepository.UpdatePatterns(model);
                return RedirectToAction("Index", "Patterns");
            }
            else
            {
                ViewBag.Action = (model.PatternId == 0) ? "Add" : "Edit";
                ViewBag.Level = context.Levels.OrderBy(l => l.LevelId).ToList();
                ViewBag.Hook = context.Hooks.OrderBy(h => h.HookId).ToList();
                return View(model);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(int patternId)
        {
            var pattern = await prepository.GetPatternsByIdAsync(patternId);
            return View(pattern);
        }

        [HttpPost]
        public IActionResult Delete(PatternModel model)
        {
            prepository.DeletePatterns(model.PatternId);
            return RedirectToAction("Index", "Patterns");
        }
    }
}