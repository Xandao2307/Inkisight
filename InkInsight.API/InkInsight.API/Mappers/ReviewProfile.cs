using AutoMapper;
using InkInsight.API.Dto;
using InkInsight.API.Entities;

namespace InkInsight.API.Mappers
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review,ReviewDTO>();
            CreateMap<ReviewDTO, Review>();
        }
    }
}
