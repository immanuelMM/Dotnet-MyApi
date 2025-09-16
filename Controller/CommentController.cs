using Microsoft.AspNetCore.Mvc;
using MyApi.Data;
using MyApi.Dtos.Comment;
using MyApi.Interface;
using MyApi.Mappers;

namespace MyApi.Controller
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ApplicationDBContext _context;

        public CommentController(ApplicationDBContext context, ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            var comments = await _commentRepository.GetAllCommentsAsync();
            var commentDDto = comments.Select(c => c.ToCommentDto());
            return Ok(commentDDto);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            var comment = await _commentRepository.GetCommentsByIdAsync(id);
            if (comment == null) return NotFound();

            return Ok(comment.ToCommentDto());
        }

        [HttpPost]
        public async Task<IActionResult> createComment(int StockId, [FromBody] createCommentRequestDTo commentDto)
        {
            var stockExist = await _context.Stocks.FindAsync(StockId);

            if (stockExist == null)
            {
                return NotFound();
            }

            var commentModel = commentDto.ToCommentFromCreateDTO();

            await _commentRepository.AddCommentAsync(StockId, commentModel);

            return Ok(commentModel);

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> upDateComment(int id, [FromBody] createCommentRequestDTo comments)
        {
            var comment = await _commentRepository.UpdateComment(id, comments.ToCommentFromCreateDTO());
            if (comment == null)
            {
                return NotFound("comment not found");
            }

            return Ok(comment);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var deleteComment = await _commentRepository.DeleteCommentAsync(id);

            if (!deleteComment)
            {
                return NotFound("No ID");
            }

            return NoContent();
        }
    }
}