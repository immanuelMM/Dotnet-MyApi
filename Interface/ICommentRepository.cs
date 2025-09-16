using MyApi.Dtos.Comment;
using MyApi.Models;

namespace MyApi.Interface
{
    public interface ICommentRepository
    {
        Task<Comments> AddCommentAsync(int stockid, Comments comment);
        Task<Comments?> GetCommentsByIdAsync(int Id);
        Task<bool> DeleteCommentAsync(int commentId);
        Task<List<Comments>> GetAllCommentsAsync();
        Task<Comments?> UpdateComment(int Id, Comments comment);
    }
}