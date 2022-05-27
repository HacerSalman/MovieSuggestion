using AutoMapper;
using MovieSuggestion.Core.DTO;
using MovieSuggestion.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieSuggestion.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserUpdateDTO>().ReverseMap();
            CreateMap<User, UserCreateDTO>().ReverseMap();
            CreateMap<UserMovie, UserMovieDTO>().ReverseMap();
            CreateMap<UserMovie, UserMovieUpdateDTO>().ReverseMap();
            CreateMap<UserMovie, UserMovieCreateDTO>().ReverseMap();

        }
    }
}
