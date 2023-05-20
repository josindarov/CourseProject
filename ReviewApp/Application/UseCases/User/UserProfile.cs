using Application.UseCases.User.Queries;
using AutoMapper;

namespace Application.UseCases.User;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<Domain.Entities.User, UserDto>();
        CreateMap<Domain.Entities.User, GetUserByIdResponse>();
    }
}