using AutoMapper;
using Hospital.Models;

namespace Hospital
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Article, ArticleDTO>();
            CreateMap<ArticleDTO, Article>();


        }
    }
}
