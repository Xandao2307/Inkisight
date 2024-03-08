using AutoMapper;
using InkInsight.API.Dto;
using InkInsight.API.Entities;

namespace InkInsight.API.Mappers
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<BookDTO, Book>();
            CreateMap<Book, BookDTO>();
        }
    }
}
