using Business.DTOs;
using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ShowingController : ControllerBase
    {
        private readonly IShowingService _showingService;

        public ShowingController(IShowingService showingService)
        {
            _showingService = showingService;
        }

        [HttpGet("movie/{movieId}")]
        public async Task<IActionResult> GetShowingByMovieId(Guid movieId)
        {
            var showings = await _showingService.GetShowingsByMovieId(movieId);

            return Ok(showings);
        }

        [HttpGet("{showingId}")]
        public async Task<IActionResult> GetShowingById(Guid showingId)
        {
            var showing = await _showingService.GetShowingByIdAsync(showingId);

            return Ok(showing);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddShowing(CreateShowingDto showingData)
        {
            var showing = await _showingService.AddShowingAsync(showingData);

            return Ok(showing);
        }
    }
}
