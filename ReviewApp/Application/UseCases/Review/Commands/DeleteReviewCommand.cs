using Application.Exceptions;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Review.Commands;

public class DeleteReviewCommandRequest : IRequest<DeleteReviewCommandResponse>
{
    public int Id { get; set; }
}

public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommandRequest,DeleteReviewCommandResponse>
{
    private readonly IApplicationDbContext _context;

    public DeleteReviewCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<DeleteReviewCommandResponse> Handle(DeleteReviewCommandRequest request, CancellationToken cancellationToken)
    {
        var review = await _context.Reviews.FirstOrDefaultAsync(o => o.Id == request.Id,cancellationToken);
        if (review == null)
        {
            throw new NullException($"There is no review with this {request.Id} id");
        }

       _context.Reviews.Remove(review);
       await _context.SaveChangesAsync(cancellationToken);
       return new DeleteReviewCommandResponse()
       {
           IsDelete = true
       };

    }
}

public class DeleteReviewCommandResponse
{
    public bool IsDelete { get; set; }
}

