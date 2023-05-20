using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>, IApplicationDbContext
{
    public DbSet<Review> Reviews { get; set; }
    
    public DbSet<Comment> Comments { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>()
            .HasMany(p => p.Reviews)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId);
        
        builder.Entity<Review>()
            .HasMany(p => p.Comments)
            .WithOne(p => p.Review)
            .HasForeignKey(p => p.ReviewId);
        
        base.OnModelCreating(builder);
    }
}