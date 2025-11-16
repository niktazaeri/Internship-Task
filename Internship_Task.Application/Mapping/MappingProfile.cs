using AutoMapper;
using Internship_Task.Application.DTOs;
using Internship_Task.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_Task.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User,RegisterDTO>().ReverseMap();
            CreateMap<LoginDTO,User>().ReverseMap();
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.Username , opt => opt.MapFrom(opt => opt.User.UserName))
                .ReverseMap();
            CreateMap<CreateProductDTO , Product>().ReverseMap();
            CreateMap<UpdateProductDTO , Product>().ReverseMap();
        }
    }
}
