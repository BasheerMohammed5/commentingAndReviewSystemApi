using commentingAndReviewSystem.Application.Interfaces;
using commentingAndReviewSystem.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace ReviewManagementSystem.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task PostReview(int userId, int contentItemId, string comment, int rating) // تعديل هنا
        {
            if (rating < 1 || rating > 5)
                throw new ArgumentException("Rating must be between 1 and 5");

            var review = new Review
            {
                UserId = userId.ToString(), // تحويل userId إلى string إذا لزم الأمر
                ContentItemId = contentItemId,
                Comment = comment,
                Rating = rating,
                Timestamp = DateTime.UtcNow
            };

            await _reviewRepository.AddReviewAsync(review);
        }

        public async Task EditReview(int reviewId, int userId, string comment, int rating) // تعديل هنا
        {
            var review = await _reviewRepository.GetReviewByIdAsync(reviewId);

            if (review.UserId != userId.ToString()) // تحويل userId إلى string إذا لزم الأمر
                throw new UnauthorizedAccessException("Cannot edit other user's review");

            review.Comment = comment;
            review.Rating = rating;
            review.Timestamp = DateTime.UtcNow;

            await _reviewRepository.UpdateReviewAsync(review);
        }

        public async Task DeleteReview(int reviewId, int userId) // تعديل هنا
        {
            var review = await _reviewRepository.GetReviewByIdAsync(reviewId);

            if (review.UserId != userId.ToString()) // تحويل userId إلى string إذا لزم الأمر
                throw new UnauthorizedAccessException("Cannot delete other user's review");

            await _reviewRepository.DeleteReviewAsync(review);
        }
    }
}
