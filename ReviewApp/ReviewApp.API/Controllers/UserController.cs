using Application.UseCases.User.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ReviewApp.API.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly UserManager<User> _userManager;

    public UserController(IMediator mediator, UserManager<User> userManager)
    {
        _mediator = mediator;
        _userManager = userManager;
    }

    [HttpGet("GetListOfUser")]
    public async Task<ActionResult<GetListOfUsersResponse>> GetAllUser()
    {
        var result = await _mediator.Send(new GetListOfUsersQueryRequest());
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _mediator.Send(new GetUserByIdResponse(){Id = id});
        return Ok(user);
    }

    [HttpPost("Register")]
    public async Task<IActionResult> RegisterUser(UserRegister userRegister)
    {
        var user = new User()
        {
            UserName = userRegister.UserName,
            Email = userRegister.Email,
            FirstName = userRegister.FirstName,
            LastName = userRegister.LastName
        };

        var result = await _userManager.CreateAsync(user, userRegister.Password);
        if (!result.Succeeded)
            return Ok("Something went wrong");
        
        return Ok("User is succesfully registered");
    }
}
