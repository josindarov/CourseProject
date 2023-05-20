using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Comment.Queries;

public class GetCommentByIdQueryRequest : IRequest<GetCommentByIdQueryResponse>
{
    public int Id { get; set; }
}

public class GetCommentByIdQueryHandler : IRequestHandler<GetCommentByIdQueryRequest, GetCommentByIdQueryResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCommentByIdQueryHandler(IMapper mapper, IApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    
    public async Task<GetCommentByIdQueryResponse> Handle(GetCommentByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var comment = await _context.Comments.FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);
        if (comment == null)
        {
            throw new NotFoundException($"There is no comment with this {request.Id} id");
        }

        var result = _mapper.Map<GetCommentByIdQueryResponse>(comment);
        return result;
    }
}

public class GetCommentByIdQueryResponse
{
    public int Id { get; set; }
    
    public string? ContentOfComment { get; set; }
    
    public DateTimeOffset DateOfRegister { get; set; }
    
    public int ReviewId { get; set; }
    
    public string? TitleOfReview { get; set; }
}
