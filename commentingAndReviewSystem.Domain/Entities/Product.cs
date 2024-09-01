using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace commentingAndReviewSystem.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string Description { get; set; }

        // Define relationships
        public List<Review> Reviews { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
