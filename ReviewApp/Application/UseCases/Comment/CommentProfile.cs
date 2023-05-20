using Application.UseCases.Comment.Commands;
using Application.UseCases.Comment.Queries;
using AutoMapper;

namespace Application.UseCases.Comment;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<CreateCommentCommandRequest, Domain.Entities.Comment>();

        CreateMap<Domain.Entities.Comment, CreateCommentCommandResponse>()
            .ForMember(dest => dest.TitleOfReview, configs => 
                configs.MapFrom(src => src.Review.TitleOfReview));
        
        CreateMap<Domain.Entities.Comment, CommentDto>()
            .ForMember(dest => dest.TitleOfReview, configs => 
                configs.MapFrom(src => src.Review.TitleOfReview));

        CreateMap<Domain.Entities.Comment, GetCommentByIdQueryResponse>()
            .ForMember(dest => dest.TitleOfReview, configs =>
                configs.MapFrom(src => src.Review.TitleOfReview));

    }
}