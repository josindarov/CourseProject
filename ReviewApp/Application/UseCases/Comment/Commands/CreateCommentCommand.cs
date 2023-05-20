using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Application.UseCases.Comment.Commands;

public class CreateCommentCommandRequest : IRequest<CreateCommentCommandResponse>
{
    public string ContentOfComment { get; set; }
    public int ReviewId { get; set; }
}

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommandRequest,CreateCommentCommandResponse>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;

    public CreateCommentCommandHandler(IMapper mapper, IApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    
    public async Task<CreateCommentCommandResponse> Handle(CreateCommentCommandRequest request, CancellationToken cancellationToken)
    {
        var comment = _mapper.Map<Domain.Entities.Comment>(request);
        comment.DateOfRegister = DateTime.UtcNow;
        await _context.Comments.AddAsync(comment, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    
        var result = _mapper.Map<CreateCommentCommandResponse>(comment);
        return result;
    }
}

public class CreateCommentCommandResponse
{
    public int Id { get; set; }
    
    public string ContentOfComment { get; set; }
    
    public DateTimeOffset DateOfRegister { get; set; }
    
    public int ReviewId { get; set; }
    
    public string? TitleOfReview { get; set; }
}