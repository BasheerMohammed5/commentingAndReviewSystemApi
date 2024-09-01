using System;
using System.ComponentModel.DataAnnotations;

namespace commentingAndReviewSystem.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Content is required.")]
        [MaxLength(1000, ErrorMessage = "Content cannot be longer than 1000 characters.")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Timestamp is required.")]
        public DateTime Timestamp { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required(ErrorMessage = "Content Item ID is required.")]
        public int ContentItemId { get; set; }
        public ContentItem ContentItem { get; set; }


        [Required(ErrorMessage = "Product ID is required.")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
