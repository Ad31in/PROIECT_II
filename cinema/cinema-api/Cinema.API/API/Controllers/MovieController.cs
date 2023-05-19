using Business.DTOs;
using Business.Interfaces;
using DataLogic.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMovies()
        {
            var movies = await _movieService.GetAllMovies();

            return Ok(movies);
        }

        [HttpGet("{movieId}")]
        public async Task<IActionResult> GetMovieById(Guid movieId)
        {
            var movie = await _movieService.GetMovieById(movieId);

            return Ok(movie);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddMovie(CreateMovieDto movieData)
        {
            var movie = await _movieService.AddMovie(movieData);

            return Ok(movie);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("{movieId}/addPhoto")]
        public async Task<IActionResult> AddPhotoToMovie(IFormFile file, Guid movieId)
        {
            var movie = await _movieService.AddPhotoToMovieAsync(file, movieId);
            return Ok(movie);
        }


    }
}
