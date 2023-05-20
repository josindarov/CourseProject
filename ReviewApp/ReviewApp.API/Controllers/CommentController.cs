using Application.UseCases.Comment.Commands;
using Application.UseCases.Comment.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ReviewApp.API.Controllers;

[ApiController]
[Route("api/controller")]
public class CommentController : ControllerBase
{
    private readonly IMediator _mediator;

    public CommentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("CreateComment")]
    public async Task<ActionResult<CreateCommentCommandResponse>> CreateComment(CreateCommentCommandRequest request)
    {
        var comment = await _mediator.Send(request);
        return Ok(comment);
    }
    
    [HttpGet("GetList")]
    public async Task<ActionResult<GetListOfCommentQueryResponse>> GetListOfComment()
    {
        return Ok(await _mediator.Send(new GetListOfCommentsQueryRequest()));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<GetCommentByIdQueryResponse>> GetByIdComment(int id)
    {
        var comment = await _mediator.Send(new GetCommentByIdQueryRequest(){Id = id});
        return Ok(comment);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<DeleteCommentCommandResponse>> DeleteComment(int id)
    {
        var comment = await _mediator.Send(new DeleteCommentCommandRequest(){Id = id});
        return Ok(comment);
    }
}