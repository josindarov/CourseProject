namespace Domain.Entities;

public class Review
{
    public int Id { get; set; }
    
    public string TitleOfReview { get; set; }
    
    public string ContentOfReview { get; set; }
    
    public double Rating { get; set; }
    
    public int UserId { get; set; }
    
    public User User { get; set; }
    
    public ICollection<Comment> Comments { get; set; }
}