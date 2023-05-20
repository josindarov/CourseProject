namespace Domain.Entities;

public class Comment
{
    public int Id { get; set; }
    
    public string ContentOfComment { get; set; }
    
    public DateTimeOffset DateOfRegister { get; set; }
    
    public int ReviewId { get; set; }
    
    public Review Review { get; set; }
}