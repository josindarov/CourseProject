using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Comment.Queries;

public class GetListOfCommentsQueryRequest : IRequest<GetListOfCommentQueryResponse>
{
    
}

public class GetListOfCommentsQueryHandler : IRequestHandler<GetListOfCommentsQueryRequest, GetListOfCommentQueryResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetListOfCommentsQueryHandler(IMapper mapper, IApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<GetListOfCommentQueryResponse> Handle(GetListOfCommentsQueryRequest request, CancellationToken cancellationToken)
    {
        var listOfComments = _context.Comments.AsQueryable();

        return new GetListOfCommentQueryResponse()
        {
            Results = await listOfComments.ProjectTo<CommentDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
        };
    }
}

public class GetListOfCommentQueryResponse
{
    public IEnumerable<CommentDto> Results { get; set; }
}

public class CommentDto
{
    public int Id { get; set; }
    
    public string ContentOfComment { get; set; }
    
    public DateTimeOffset DateOfRegister { get; set; }
    
    public int ReviewId { get; set; }
    
    public string TitleOfReview { get; set; }
}