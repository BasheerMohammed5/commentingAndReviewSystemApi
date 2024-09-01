
using System.ComponentModel.DataAnnotations;


namespace commentingAndReviewSystem.Domain.Entities
{
    public class ContentItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(100, ErrorMessage = "Title cannot be longer than 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string Description { get; set; }

        public List<Review> Reviews { get; set; }
    }
}
