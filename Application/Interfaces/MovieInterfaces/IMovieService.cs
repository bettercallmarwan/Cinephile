using Application.DTOs.LogDTOs;
using Application.DTOs.MovieDTOs;
using Domain.Entities;

namespace Application.Interfaces.MovieInterfaces
{
    public interface IMovieService
    {
        Task<AddedMovieResponseDto> AddMovieAsync(AddOrEditMovieRequestDto movie);
        Task<GetMovieResponseDto> GetMovieAsync(int id);
        Task<IEnumerable<GetMovieResponseDto>> GetMoviesAsync();
        Task<GetMovieResponseDto> UpdateMovieAsync(int id, AddOrEditMovieRequestDto requestDto);
        Task DeleteMovieAsync(int id);
        Task UpdateMovieRatingAndMembers(MovieDiary movieDiary, AddLogRequestDto requestDto);
    }
}
