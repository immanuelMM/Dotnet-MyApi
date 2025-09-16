using MyApi.Interface;
using MyApi.Models;
using MyApi.Data;
using Microsoft.EntityFrameworkCore;
using MyApi.Dtos.Comment;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MyApi.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;

        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Comments> AddCommentAsync(int stockId, Comments comment)
        {  
            comment.StockId = stockId;
            await _context.AddAsync(comment);
            await _context.SaveChangesAsync();

            return comment;
        }

        public async Task<bool> DeleteCommentAsync(int commentId)
        {
            var commentsExist = await _context.Comments.FindAsync(commentId);

            if (commentsExist == null)
            {
                return false;
            }

            _context.Comments.Remove(commentsExist);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Comments>> GetAllCommentsAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comments?> GetCommentsByIdAsync(int Id)
        {
            return await _context.Comments.Include(a => a.Stock).FirstOrDefaultAsync(c => c.Id == Id);
        }

        public async Task<Comments?> UpdateComment(int Id, Comments comment)
        {
            var commentsExist = await _context.Comments.FindAsync(Id);

            if (commentsExist == null)
            {
                return null;
            }

            commentsExist.Title = comment.Title;
            commentsExist.Content = commentsExist.Content;
            commentsExist.CreatedAt = commentsExist.CreatedAt;
            
            await _context.SaveChangesAsync();

            return commentsExist;
        }
    }
}