using commentingAndReviewSystem.Domain.Entities;
using System.ComponentModel.DataAnnotations;

public class Review
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Title is required.")]
    [MaxLength(20, ErrorMessage = "Title cannot be longer than 20 characters.")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Comment is required.")]
    [MaxLength(1000, ErrorMessage = "Comment cannot be longer than 1000 characters.")]
    public string Comment { get; set; }

    [Required(ErrorMessage = "Rating is required.")]
    [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5 stars.")]
    public int Rating { get; set; }

    public DateTime Timestamp { get; set; }

    [Required(ErrorMessage = "User ID is required.")]
    public string UserId { get; set; }
    public virtual User? User { get; set; }

    [Required(ErrorMessage = "Product ID is required.")]
    public int ProductId { get; set; }
    public Product Product { get; set; }


    [Required(ErrorMessage = "Content Item ID is required.")]
    public int? ContentItemId { get; set; }
    public ContentItem ContentItem { get; set; }



}
