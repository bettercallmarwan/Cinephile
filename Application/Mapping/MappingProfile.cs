using Application.DTOs.GenreDTOs;
using Application.DTOs.LogDTOs;
using Application.DTOs.MovieDiaryDTOs;
using Application.DTOs.MovieDTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Movie
            CreateMap<Movie, AddedMovieResponseDto>();
            CreateMap<AddOrEditMovieRequestDto, Movie>();
            CreateMap<Movie, GetMovieResponseDto>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
            #endregion

            #region Genre
            CreateMap<Genre, GenreDto>().ReverseMap();
            #endregion

            #region Log
            CreateMap<AddLogRequestDto, Log>();
            CreateMap<Log, AddLogResponseDto>();
            CreateMap<Log, GetLogResponseDto>(); 
            #endregion
        }
    }
}
