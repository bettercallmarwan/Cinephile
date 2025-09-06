using Application.DTOs.MovieDTOs;
using Application.Interfaces.MovieInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cinephile.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController(IMovieService movieService) : ControllerBase
    {
        [HttpGet("GetMovie/{id:int}")]
        public async Task<ActionResult<GetMovieResponseDto>> GetMovie(int id)
        {
            var movie = await movieService.GetMovieAsync(id);
            return Ok(movie);
        }

        [HttpGet("GetMovies")]
        public async Task<ActionResult<IEnumerable<GetMovieResponseDto>>> GetMovies()
        {
            var movies = await movieService.GetMoviesAsync();
            return Ok(movies);
        }

        [HttpPut("UpdateMovie/{id:int}")]
        public async Task<ActionResult<GetMovieResponseDto>> UpdateMovie(int id, AddOrEditMovieRequestDto requestDto)
        {
            var movie = await movieService.UpdateMovieAsync(id, requestDto);
            return Ok(movie);
        }

        [HttpPost("CreateMovie")]
        public async Task<ActionResult<AddedMovieResponseDto>> AddMovie(AddOrEditMovieRequestDto requestDto)
        {
            var movie = await movieService.AddMovieAsync(requestDto);
            return Ok(movie);
        }

        [HttpDelete("DeleteMovie/{id:int}")]
        public async Task<ActionResult> DeleteMovie(int id)
        {
            await movieService.DeleteMovieAsync(id);
            return NoContent();
        }
    }
}
