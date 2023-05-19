using Business.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IMovieService
    {
        Task<MovieDto> GetMovieById(Guid Id);

        Task<ICollection<MovieDto>> GetAllMovies();

        Task<MovieDto> AddMovie(CreateMovieDto movieDto);

        Task<MovieDto> AddPhotoToMovieAsync(IFormFile photo, Guid movieId);
    }
}
