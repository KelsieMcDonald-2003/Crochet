using Crochet.Data;
using Crochet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Emit;
using System.Collections.Generic;

namespace Crochet.Data
{
    public class AppDbContext : IdentityDbContext<AppUserModel>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<HookModel> Hooks { get; set; }
        public DbSet<LevelModel> Levels { get; set; }
        public DbSet<CommentModel> Comments { get; set; }
        public DbSet<PatternModel> Patterns { get; set; }
        public DbSet<PhotoModel> Photos { get; set; }
        public DbSet<RecommendationModel> Recommendations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<HookModel>().HasData(
                    new HookModel { HookId = "A", HookName = "B", HookSize = "2.25 mm" },
                    new HookModel { HookId = "B", HookName = "C", HookSize = "2.75 mm" },
                    new HookModel { HookId = "C", HookName = "D", HookSize = "3.25 mm" },
                    new HookModel { HookId = "D", HookName = "E", HookSize = "3.5 mm" },
                    new HookModel { HookId = "E", HookName = "F", HookSize = "3.75 mm" },
                    new HookModel { HookId = "F", HookName = "G", HookSize = "4.00 mm" },
                    new HookModel { HookId = "G", HookName = "H", HookSize = "5.00 mm" },
                    new HookModel { HookId = "H", HookName = "I", HookSize = "5.50 mm" },
                    new HookModel { HookId = "I", HookName = "J", HookSize = "6.00 mm" },
                    new HookModel { HookId = "J", HookName = "K", HookSize = "6.50 mm" },
                    new HookModel { HookId = "K", HookName = "L", HookSize = "8.00 mm" },
                    new HookModel { HookId = "L", HookName = "M", HookSize = "9.00 mm" },
                    new HookModel { HookId = "M", HookName = "N", HookSize = "10.00 mm" }
                    //new HookModel { HookId = "N", HookName = "B", HookSize = "2.25 mm" },
                    //new HookModel { HookId = "O", HookName = "B", HookSize = "2.25 mm" }
                );

            builder.Entity<LevelModel>().HasData(
                    new LevelModel { LevelId = "A", LevelName = "Apprentice" },
                    new LevelModel { LevelId = "B", LevelName = "Beginner" },
                    new LevelModel { LevelId = "C", LevelName = "Intermediate" },
                    new LevelModel { LevelId = "D", LevelName = "Advanced" },
                    new LevelModel { LevelId = "E", LevelName = "Expert" },
                    new LevelModel { LevelId = "F", LevelName = "Mastery" }
                );

            builder.Entity<IdentityUserLogin<string>>().HasKey(l => new { l.LoginProvider, l.ProviderKey });
            base.OnModelCreating(builder);
        }
    }
}