using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser<int>
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public double ReviewRating { get; set; }
    
    public ICollection<Review> Reviews { get; set; }
}