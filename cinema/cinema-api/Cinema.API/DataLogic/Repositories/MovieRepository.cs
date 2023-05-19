using DataLogic.Data;
using DataLogic.Entities;
using DataLogic.Interfaces;
using DatingApp.Common.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly DataContext _dataContext;

    public MovieRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<ICollection<Movie>> GetAllAsync()
    {
        return await _dataContext.Movies
            .Include(m => m.Photo)
            .Include(m => m.Showings)
            .ThenInclude(s => s.Room)
            .Include(m => m.Genres)
            .ToListAsync();
    }

    public async Task<Movie> GetById(Guid Id)
    {
        return await _dataContext.Movies
            .Include(m => m.Photo)
            .Include(m => m.Showings)
            .Include(m => m.Genres)
            .FirstAsync(m => m.Id == Id);
    }

    public async Task<Movie> AddMovie(Movie movie)
    {
        await _dataContext.Movies.AddAsync(movie);

        var result = await _dataContext.SaveChangesAsync();

        if (result > 0)
        {
            return movie;
        }

        throw new ApiException
        {
            StatusCode = StatusCodes.Status400BadRequest,
            Message = "There is a problem adding the movie"
        };
    }

    public async Task<Movie> AddPhotoAsync(Photo photo, Movie movie)
    {
        movie.Photo = photo;
        movie.PhotoId = photo.Id;

        bool response = await _dataContext.SaveChangesAsync() > 0;

        if (response)
        {
            return movie;
        }

        throw new ApiException
        {
            StatusCode = StatusCodes.Status400BadRequest,
            Message = "There is a problem adding the photo"
        };
    }

}

