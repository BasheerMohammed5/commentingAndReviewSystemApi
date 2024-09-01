using commentingAndReviewSystem.Application.Interfaces;
using commentingAndReviewSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ReviewManagementSystem.Infrastructure.Data;


namespace ReviewManagementSystem.Infrastructure.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly AppDbContext _context;

        public ReviewRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddReviewAsync(Review review)
        {
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
        }

        public async Task<Review> GetReviewByIdAsync(int reviewId)
        {
            return await _context.Reviews.Include(r => r.User)
                                          .Include(r => r.ContentItem)
                                          .FirstOrDefaultAsync(r => r.Id == reviewId);
        }

        public async Task UpdateReviewAsync(Review review)
        {
            _context.Reviews.Update(review);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReviewAsync(Review review)
        {
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
        }
    }
}
