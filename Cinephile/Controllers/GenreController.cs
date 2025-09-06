using Application.DTOs.GenreDTOs;
using Application.Interfaces.GenreInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cinephile.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;   
        }

        [HttpGet("GetGenres")]
        public async Task<ActionResult<IEnumerable<GenreDto>>> GetGenres()
        {
            var genres = await _genreService.GetGenresAsync();
            return Ok(genres);
        }

        [HttpPost("CreateGenre")]
        public async Task<ActionResult<GenreDto>> AddGenre(GenreDto genreDto)
        {
            var genre = await _genreService.AddGenreAsync(genreDto);
            return Ok(genre);
        }

        [HttpDelete("DeleteGenre/{id:int}")]
        public async Task<ActionResult> DeleteGenre(int id)
        {
            await _genreService.DeleteGenre(id);
            return NoContent();
        }
    }
}
