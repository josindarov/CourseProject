using Application.UseCases.Review.Commands;
using Application.UseCases.Review.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ReviewApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReviewController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<CreateReviewCommandResponse>> CreateReview(CreateReviewCommandRequest request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpGet("ListOfReview")]
    public async Task<ActionResult<GetListOfReviewQueryResponse>> GetListOfReviews()
    {
        var result = await _mediator.Send(new GetListOfReviewQueryRequest());
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<GetReviewByIdQueryResponse>> GetReviewById(int id)
    {
        var review = await _mediator.Send(new GetReviewByIdQueryRequest() { Id = id });
        return review;
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<DeleteReviewCommandResponse>> DeleteReview(int id)
    {
        return await _mediator.Send(new DeleteReviewCommandRequest() { Id = id });
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<UpdateReviewCommandResponse>> Update(int id, UpdateReviewCommandRequest request)
    {
        if (request.UserId != id)
        {
            return BadRequest("Id's from url and from body are different");
        }

        return Ok(await _mediator.Send(request));
    }

}