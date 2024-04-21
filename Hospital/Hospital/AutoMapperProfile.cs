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

            CreateMap<Department, DepartmentDTO>();
            CreateMap<DepartmentDTO, Department>();

            CreateMap<Doctor, DoctorDTO>();
            CreateMap<DoctorDTO, Doctor>();

            CreateMap<Review, ReviewDTO>();
            CreateMap<ReviewDTO, Review>();

            CreateMap<Role, RoleDTO>();
            CreateMap<RoleDTO, Role>();

            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();

            CreateMap<Wish, WishDTO>();
            CreateMap<WishDTO, Wish>();
        }
    }
}
