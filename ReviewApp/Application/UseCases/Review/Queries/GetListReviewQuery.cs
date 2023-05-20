using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Review.Queries;

public class GetListOfReviewQueryRequest : IRequest<GetListOfReviewQueryResponse>
{
    
}

public class GetListOfReviewQueryHandler : IRequestHandler<GetListOfReviewQueryRequest,GetListOfReviewQueryResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetListOfReviewQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<GetListOfReviewQueryResponse> Handle(GetListOfReviewQueryRequest request, CancellationToken cancellationToken)
    {
        var listOfReview = _context.Reviews.AsQueryable();
        var result = new GetListOfReviewQueryResponse()
        {
            Results = await listOfReview.ProjectTo<ReviewDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
        };
        
        return result;
    }
}

public class GetListOfReviewQueryResponse
{
    public List<ReviewDto> Results { get; set; }
}

public class ReviewDto
{
    public int Id { get; set; }
    
    public string TitleOfReview { get; set; }
    
    public string ContentOfReview { get; set; }
    
    public double Rating { get; set; }
    
    public int UserId { get; set; }
    
    public string UserName { get; set; }
}