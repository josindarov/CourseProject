using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.User.Queries;

public class GetListOfUsersQueryRequest : IRequest<GetListOfUsersResponse>
{
    
}

public class GetListOfUsersQueryHandler : IRequestHandler<GetListOfUsersQueryRequest, GetListOfUsersResponse>
{
    private readonly IMapper _mapper;
    private readonly UserManager<Domain.Entities.User> _userManager;

    public GetListOfUsersQueryHandler(IMapper mapper, UserManager<Domain.Entities.User> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }
    public async Task<GetListOfUsersResponse> Handle(GetListOfUsersQueryRequest request, CancellationToken cancellationToken)
    {
        var listOfUsers = await _userManager.Users
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new GetListOfUsersResponse()
        {
            Result = listOfUsers
        };
    }
}
public class GetListOfUsersResponse
{
    public IEnumerable<UserDto> Result { get; set; }
}

public class UserDto
{
    public int Id { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string UserName { get; set; }
    
    public string Email { get; set; }
}
    