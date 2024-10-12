using AutoMapper;
using LibraryManagement.Database.Entities;
using LibraryManagement.Services.Dtos;

namespace LibraryManagement.Services.Mappers
{
    public class BookMapper : Profile
    {
        public BookMapper()
        {
            CreateMap<RequestBookDto,Book>().ReverseMap();
            CreateMap<Book,ResponseBookDto>().ReverseMap();
            CreateMap<RequestPaginationDto,PagingMetadata>().ReverseMap();
        }
    }
}
