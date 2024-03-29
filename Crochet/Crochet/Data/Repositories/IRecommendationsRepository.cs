using Crochet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Crochet.Data.Repositories
{
    public interface IRecommendationsRepository
    {
        public List<RecommendationModel> GetRecommendations();
        public Task<RecommendationModel> GetRecommendationsByIdAsync(int id);
        public Task<int> StoreRecommendationsAsync(RecommendationModel model);
        public int UpdateRecommendations(RecommendationModel model);
        public int DeleteRecommendations(int id);
    }

    public class RecommendationsRepository : IRecommendationsRepository
    {
        AppDbContext context;
        public RecommendationsRepository(AppDbContext c)
        {
            context = c;
        }

        public List<RecommendationModel> GetRecommendations()
        {
            return context.Recommendations.ToList();
        }

        public async Task<RecommendationModel> GetRecommendationsByIdAsync(int id)
        {
            var recommend = await context.Recommendations.FindAsync(id);
            return recommend;
        }

        public async Task<int> StoreRecommendationsAsync(RecommendationModel model)
        {
            await context.AddAsync(model);
            return await context.SaveChangesAsync();
        }

        public int UpdateRecommendations(RecommendationModel model)
        {
            context.Recommendations.Update(model);
            return context.SaveChanges();
        }

        public int DeleteRecommendations(int modelId)
        {
            RecommendationModel recommend = GetRecommendationsByIdAsync(modelId).Result;
            context.Recommendations.Remove(recommend);
            return context.SaveChanges();
        }
    }

    public class FakeRecommendationsRepository : IRecommendationsRepository
    {
        List<RecommendationModel> recommendations = new List<RecommendationModel>();

        public int DeleteRecommendations(int modelId)
        {
            throw new NotImplementedException();
        }

        public async Task<RecommendationModel> GetRecommendationsByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public List<RecommendationModel> GetRecommendations()
        {
            throw new NotImplementedException();
        }

        public int UpdateRecommendations(RecommendationModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<int> StoreRecommendationsAsync(RecommendationModel model)
        {
            model.RecommendId = recommendations.Count + 1;
            recommendations.Add(model);
            return model.RecommendId;
        }
    }
}