using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace commentingAndReviewSystem.Application.Interfaces
{
    public interface IReviewService
    {
        Task PostReview(int userId, int contentItemId, string comment, int rating);
        Task EditReview(int reviewId, int userId, string comment, int rating);
        Task DeleteReview(int reviewId, int userId);
    }
}
