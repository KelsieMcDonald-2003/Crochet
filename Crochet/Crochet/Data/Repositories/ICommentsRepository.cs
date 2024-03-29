using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Crochet.Models;
using System.Runtime.CompilerServices;

namespace Crochet.Data.Repositories
{
    public interface ICommentsRepository
    {
        public List<CommentModel> GetComments();
        public Task<CommentModel> GetCommentsByIdAsync(int id);
        public Task<int> StoreCommentsAsync(CommentModel comment);
    }

    public class CommentsRepository : ICommentsRepository
    {
        AppDbContext context;
        public CommentsRepository(AppDbContext c)
        {
            context = c;
        }

        public async Task<CommentModel> GetCommentsByIdAsync(int id)
        {
            var comment = await context.Comments.FindAsync(id);
            return comment;
        }

        public List<CommentModel> GetComments()
        {
            return context.Comments.ToList();
        }

        public async Task<int> StoreCommentsAsync(CommentModel model)
        {
            await context.AddAsync(model);
            return await context.SaveChangesAsync();
        }
    }

    public class FakeCommentsRepository : ICommentsRepository
    {
        List<CommentModel> comments = new List<CommentModel>();
        public List<CommentModel> GetComments()
        {
            throw new NotImplementedException();
        }

        public async Task<CommentModel> GetCommentsByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> StoreCommentsAsync(CommentModel model)
        {
            model.CommentId = comments.Count + 1;
            comments.Add(model);
            return model.CommentId;
        }
    }
}