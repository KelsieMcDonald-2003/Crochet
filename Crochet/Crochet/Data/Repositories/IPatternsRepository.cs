using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Crochet.Models;

namespace Crochet.Data.Repositories
{
    public interface IPatternsRepository
    {
        public List<PatternModel> GetPatterns();
        public Task<PatternModel> GetPatternsByIdAsync(int patternId);
        public Task<int> StorePatternsAsync(PatternModel pattern);
        public int UpdatePatterns(PatternModel pattern);
        public int DeletePatterns(int patternId);
    }

    public class PatternsRepository : IPatternsRepository
    {
        AppDbContext context;
        public PatternsRepository(AppDbContext c)
        {
            context = c;
        }

        public List<PatternModel> GetPatterns()
        {
            return context.Patterns
                .Include(h => h.Hook)
                .Include(l => l.Level)
                .Include(c => c.Comments)
                .Include(u => u.User)
                .ToList();
        }

        public async Task<PatternModel> GetPatternsByIdAsync(int id)
        {
            var pattern = await context.Patterns.FindAsync(id);
            return pattern;
        }

        public async Task<int> StorePatternsAsync(PatternModel model)
        {
            await context.AddAsync(model);
            return await context.SaveChangesAsync();
        }

        public int UpdatePatterns(PatternModel model)
        {
            context.Update(model);
            return context.SaveChanges();
        }

        public int DeletePatterns(int patternId)
        {
            PatternModel pattern = context.Patterns.Include(c => c.Comments).SingleOrDefault(c => c.PatternId == patternId);
            context.Comments.RemoveRange(pattern.Comments);
            context.Patterns.Remove(pattern);
            return context.SaveChanges();
        }
    }

    public class FakePatternsRepository : IPatternsRepository
    {
        List<PatternModel> patterns = new List<PatternModel>();
        public int DeletePatterns(int modelId)
        {
            throw new NotImplementedException();
        }

        public async Task<PatternModel> GetPatternsByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public List<PatternModel> GetPatterns()
        {
            throw new NotImplementedException();
        }

        public async Task<int> StorePatternsAsync(PatternModel pattern)
        {
            pattern.PatternId = patterns.Count + 1;
            patterns.Add(pattern);
            return pattern.PatternId;
        }

        public int UpdatePatterns(PatternModel model)
        {
            throw new NotImplementedException();
        }
    }
}