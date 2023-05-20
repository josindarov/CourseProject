using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Review.Commands;

public class UpdateReviewCommandRequest : IRequest<UpdateReviewCommandResponse>
{
    public int Id { get; set; }
    public string TitleOfReview { get; set; }
    public string ContentOfReview { get; set; }
    public int UserId { get; set; }
}

public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommandRequest,UpdateReviewCommandResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateReviewCommandHandler(IMapper mapper, IApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<UpdateReviewCommandResponse> Handle(UpdateReviewCommandRequest request, CancellationToken cancellationToken)
    {
        var review = await _context.Reviews.FirstOrDefaultAsync(o => 
            o.Id == request.Id, cancellationToken);

        if (review == null)
        {
            throw new NullException($"There is no review with this {request.Id} id");
        }

        review = _mapper.Map(request, review);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<UpdateReviewCommandResponse>(review);
    }
}

public class UpdateReviewCommandResponse
{
    public int Id { get; set; }
    public string TitleOfReview { get; set; }
    public string ContentOfReview { get; set; }
    public int UserId { get; set; }
}