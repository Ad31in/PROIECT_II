using DataLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Interfaces
{
    public interface IMovieRepository
    {
        Task<Movie> GetById(Guid movieId);

        Task<ICollection<Movie>> GetAllAsync();

        Task<Movie> AddMovie(Movie movie);

        Task<Movie> AddPhotoAsync(Photo photo, Movie movie);
    }
}
