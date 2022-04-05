using AutoMapper;
using EliteBlog.Domain.Models;
using EliteBlog.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteBlog.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Post, PostViewModel>();
            CreateMap<Post, PostDetailViewModel>();
            CreateMap<Comment, CommentViewModel>();
            CreateMap<Category, CategoryViewModel>();
            CreateMap<PostCategory, PostCategoryViewModel>();
            CreateMap<ApplicationUser, ApplicationUserViewModel>();
            CreateMap<PostCategory, CategoryViewModel>()
                .ForMember(
                    dest => dest.Title, opt => opt.MapFrom(src => src.Category.Title)
                )
                .ForMember(
                    dest => dest.ID, opt => opt.MapFrom(src => src.Category.ID)
                );
            CreateMap<Category, SelectListItem>()
                .ForMember(
                    dest => dest.Value, opt => opt.MapFrom(src => src.ID)
                )
                .ForMember(
                    dest => dest.Text, opt => opt.MapFrom(src => src.Title)
                );

        }
    }
}
