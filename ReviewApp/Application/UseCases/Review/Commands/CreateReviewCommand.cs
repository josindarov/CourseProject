using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Review.Commands;

public class CreateReviewCommandRequest : IRequest<CreateReviewCommandResponse>
{
    public string TitleOfReview { get; set; }
    
    public string ContentOfReview { get; set; }
    
    public double Rating { get; set; }
    
    public int UserId { get; set; }
}

public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommandRequest,CreateReviewCommandResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateReviewCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<CreateReviewCommandResponse> Handle(CreateReviewCommandRequest request, CancellationToken cancellationToken)
    {
        var review = _mapper.Map<Domain.Entities.Review>(request);
        
        await _context.Reviews.AddAsync(review, cancellationToken);
        
        await _context.SaveChangesAsync(cancellationToken);

        var createdReview = await _context
            .Reviews
            .Include(r => r.User)
            .FirstOrDefaultAsync(r => r.Id == review.Id, cancellationToken);

        var result = _mapper.Map<CreateReviewCommandResponse>(createdReview);
        
        return result;
    }
}

public class CreateReviewCommandResponse
{
    public int Id { get; set; }
    
    public string TitleOfReview { get; set; }
    
    public string ContentOfReview { get; set; }
    
    public double Rating { get; set; }
    
    public string? UserName { get; set; }
}