
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;



namespace commentingAndReviewSystem.Domain.Entities
{
    public class User : IdentityUser
    {
        public User()
        {
            Reviews = new List<Review>();
        }
        public virtual IList<Review> Reviews { get; set; }
    }
}
