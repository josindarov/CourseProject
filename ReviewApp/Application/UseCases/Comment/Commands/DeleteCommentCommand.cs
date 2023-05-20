using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Comment.Commands;

public class DeleteCommentCommandRequest : IRequest<DeleteCommentCommandResponse>
{
    public int Id { get; set; }
}

public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommandRequest,DeleteCommentCommandResponse>
{
    private readonly IApplicationDbContext _context;

    public DeleteCommentCommandHandler(IMapper mapper, IApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<DeleteCommentCommandResponse> Handle(DeleteCommentCommandRequest request, CancellationToken cancellationToken)
    {
        var comment = await _context.Comments.FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);

        if (comment != null)
        {
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync(cancellationToken);
            return new DeleteCommentCommandResponse()
            {
                IsDelete = true
            };
        }

        return new DeleteCommentCommandResponse()
        {
            IsDelete = false
        };

    }
}

public class DeleteCommentCommandResponse
{
    public bool IsDelete { get; set; }
}