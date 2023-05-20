using Application.Exceptions;
using Application.Interfaces;
using Application.UseCases.User.Queries;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Review.Queries;

public class GetReviewByIdQueryRequest : IRequest<GetReviewByIdQueryResponse>
{
    public int Id { get; set; }   
}

public class GetReviewByIdQueryHandler : IRequestHandler<GetReviewByIdQueryRequest,GetReviewByIdQueryResponse>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;

    public GetReviewByIdQueryHandler(IMapper mapper, IApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<GetReviewByIdQueryResponse> Handle(GetReviewByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var review = await _context.Reviews.FirstOrDefaultAsync(o 
            => o.Id == request.Id, cancellationToken);

        if (review == null)
        {
            throw new NullException($"There is no review with this {request.Id} id");
        }
        var result = _mapper.Map<GetReviewByIdQueryResponse>(review);
        
        return result;
    }
}

public class GetReviewByIdQueryResponse
{
    public int Id { get; set; }
    
    public string TitleOfReview { get; set; }
    
    public string ContentOfReview { get; set; }
    
    public double Rating { get; set; }
    
    public int UserId { get; set; }
    
    public string UserName { get; set; }
}