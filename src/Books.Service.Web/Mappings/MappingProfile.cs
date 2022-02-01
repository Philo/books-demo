using AutoMapper;
using Books.Service.Core.Entites;
using Books.Service.Web.Models;

namespace Books.Service.Web.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<BookDto, Book>()
            .ForMember(dest => dest.Id, opts => opts.Ignore())
            .ForMember(dest => dest.Title, opts => opts.MapFrom(src => src.Title))
            .ForMember(dest => dest.Author, opts => opts.MapFrom(src => src.Author))
            .ForMember(dest => dest.Price, opts => opts.MapFrom(src => src.Price));

        CreateMap<Book, BookDto>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opts => opts.MapFrom(src => src.Title))
            .ForMember(dest => dest.Author, opts => opts.MapFrom(src => src.Author))
            .ForMember(dest => dest.Price, opts => opts.MapFrom(src => src.Price));
    }
}