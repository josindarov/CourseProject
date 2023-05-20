using Application.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.User.Queries;

public class GetUserByIdQueryRequest : IRequest<GetUserByIdResponse>
{
    public int Id { get; set; }
}

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQueryRequest,GetUserByIdResponse>
{
    private readonly IMapper _mapper;
    private readonly UserManager<Domain.Entities.User> _userManager;

    public GetUserByIdQueryHandler(UserManager<Domain.Entities.User> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }
    public async Task<GetUserByIdResponse> Handle(GetUserByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);
        if (user == null)
            throw new NotFoundException($"There is no this user with this {request.Id}");

        var result = _mapper.Map<GetUserByIdResponse>(user);
        return result;
    }
}

public class GetUserByIdResponse
{
    public int Id { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Email { get; set; }
    
    public string UserName { get; set; }
}