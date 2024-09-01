using commentingAndReviewSystem.Domain.Entities;

namespace commentingAndReviewSystem.Application.Interfaces
{
    public interface IReviewRepository
    {
        Task AddReviewAsync(Review review);
        Task<Review> GetReviewByIdAsync(int reviewId);
        Task UpdateReviewAsync(Review review);
        Task DeleteReviewAsync(Review review);
    }
}
