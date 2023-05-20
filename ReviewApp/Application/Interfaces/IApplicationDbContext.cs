using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public interface IApplicationDbContext
{ 
    DbSet<Review> Reviews { get; set; }
    DbSet<Comment> Comments { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}