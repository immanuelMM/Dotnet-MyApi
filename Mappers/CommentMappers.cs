using MyApi.Dtos.Comment;
using MyApi.Models;

namespace MyApi.Mappers
{
    public static class CommentMappers
    {
        public static CommentDto ToCommentDto(this Comments comments)
        {
            return new CommentDto
            {
                Id = comments.Id,
                Title = comments.Title,
                Content = comments.Content,
                CreatedAt = comments.CreatedAt,
                StockId = comments.StockId
            };
        }
        public static Comments ToCommentFromCreateDTO(this createCommentRequestDTo commentDT0)
        {
            return new Comments
            {
                Title = commentDT0.Title,
                Content = commentDT0.Content,
                CreatedAt = commentDT0.CreatedAt
            };
        }
    }
}