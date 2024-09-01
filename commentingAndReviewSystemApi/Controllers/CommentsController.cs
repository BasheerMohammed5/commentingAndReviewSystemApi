using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReviewManagementSystem.Infrastructure.Data;
using commentingAndReviewSystem.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class CommentsController : ControllerBase
{
    private readonly AppDbContext _context;

    public CommentsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/comments
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Comment>>> GetComments(int? productId)
    {
        if (productId.HasValue)
        {
            return await _context.Comments
                .Where(c => c.ProductId == productId.Value)
                .ToListAsync();
        }
        return await _context.Comments.ToListAsync();
    }

    // GET: api/comments/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Comment>> GetComment(int id)
    {
        var comment = await _context.Comments.FindAsync(id);
        if (comment == null)
        {
            return NotFound();
        }
        return comment;
    }

    // POST: api/comments
    [HttpPost]
    public async Task<ActionResult<Comment>> PostComment(Comment comment)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Validate the existence of the product and user
        var productExists = await _context.Products.AnyAsync(p => p.Id == comment.ProductId);
        var userExists = await _context.Users.AnyAsync(u => u.Id.ToString() == comment.UserId.ToString());

        if (!productExists || !userExists)
        {
            return BadRequest("Invalid Product ID or User ID.");
        }

        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, comment);
    }

    // PATCH: api/comments/{id}
    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchComment(int id, [FromBody] JsonPatchDocument<Comment> patchDoc)
    {
        var comment = await _context.Comments.FindAsync(id);
        if (comment == null)
        {
            return NotFound();
        }

        patchDoc.ApplyTo(comment, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)ModelState);

        if (!TryValidateModel(comment))
        {
            return BadRequest(ModelState);
        }

        _context.Entry(comment).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/comments/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComment(int id)
    {
        var comment = await _context.Comments.FindAsync(id);
        if (comment == null)
        {
            return NotFound();
        }
        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
