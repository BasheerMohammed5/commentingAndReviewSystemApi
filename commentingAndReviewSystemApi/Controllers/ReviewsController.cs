using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReviewManagementSystem.Infrastructure.Data;
using commentingAndReviewSystem.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;

[Route("api/[controller]")]
[ApiController]
public class ReviewsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ReviewsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/reviews
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Review>>> GetReviews(int? productId)
    {
        if (productId.HasValue)
        {
            return await _context.Reviews
                .Where(r => r.ProductId == productId.Value)
                .ToListAsync();
        }
        return await _context.Reviews.ToListAsync();
    }

    // GET: api/reviews/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Review>> GetReview(int id)
    {
        var review = await _context.Reviews.FindAsync(id);
        if (review == null)
        {
            return NotFound();
        }
        return review;
    }

    // POST: api/reviews
    [HttpPost]
    public async Task<ActionResult<Review>> PostReview(Review review)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Validate the existence of the product and user
        var productExists = await _context.Products.AnyAsync(p => p.Id == review.ProductId);
        var userExists = await _context.Users.AnyAsync(u => u.Id == review.UserId);

        if (!productExists || !userExists)
        {
            return BadRequest("Invalid Product ID or User ID.");
        }

        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetReview), new { id = review.Id }, review);
    }

    // PATCH: api/reviews/{id}
    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchReview(int id, [FromBody] JsonPatchDocument<Review> patchDoc)
    {
        var review = await _context.Reviews.FindAsync(id);
        if (review == null)
        {
            return NotFound();
        }

        patchDoc.ApplyTo(review, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)ModelState);

        if (!TryValidateModel(review))
        {
            return BadRequest(ModelState);
        }

        _context.Entry(review).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/reviews/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReview(int id)
    {
        var review = await _context.Reviews.FindAsync(id);
        if (review == null)
        {
            return NotFound();
        }

        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
