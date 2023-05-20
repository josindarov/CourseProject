using Application.UseCases.Review.Commands;
using Application.UseCases.Review.Queries;
using AutoMapper;

namespace Application.UseCases.Review;

public class ReviewProfile : Profile
{
    public ReviewProfile()
    {
        CreateMap<CreateReviewCommandRequest,Domain.Entities.Review>();
        
        CreateMap<Domain.Entities.Review, CreateReviewCommandResponse>()
            .ForMember(dest => 
                dest.UserName, configs
                => configs.MapFrom(src => src.User.UserName));

        CreateMap<Domain.Entities.Review, ReviewDto>()
            .ForMember(dest => 
                dest.UserName, configs
                => configs.MapFrom(src => src.User.UserName));

        CreateMap<Domain.Entities.Review, GetReviewByIdQueryResponse>()
            .ForMember(dest => 
                dest.UserName, configs => 
                configs.MapFrom(src => src.User.UserName));

        CreateMap<UpdateReviewCommandRequest,Domain.Entities.Review>();
        CreateMap<Domain.Entities.Review, UpdateReviewCommandResponse>();

    }
}