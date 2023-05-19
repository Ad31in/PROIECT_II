using AutoMapper;
using Business.DTOs;
using Business.Interfaces;
using DataLogic.Entities;
using DataLogic.Enums;
using DataLogic.Interfaces;
using DatingApp.Common.Errors;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services;
public class MovieService : IMovieService
{
    private readonly IMapper _mapper;
    private readonly IMovieRepository _movieRepository;
    private readonly IPhotoService _photoService;
    private readonly IPhotoRepository _photoRepository;
    private readonly IGenreRepository _genreRepository;

    public MovieService(IGenreRepository genreRepository, IMapper mapper, IMovieRepository movieRepository, IPhotoService photoService, IPhotoRepository photoRepository)
    {
        _mapper = mapper;
        _movieRepository = movieRepository;
        _photoService = photoService;
        _photoRepository = photoRepository;
        _genreRepository = genreRepository;
    }

    public async Task<ICollection<MovieDto>> GetAllMovies()
    {
        var movies = await _movieRepository.GetAllAsync();
        var moviesDto = _mapper.Map<List<MovieDto>>(movies);

        return moviesDto;
    }

    public async Task<MovieDto> GetMovieById(Guid Id)
    {
        var movie = await _movieRepository.GetById(Id);

        if (movie == null)
        {
            throw new ApiException
            {
                StatusCode = StatusCodes.Status404NotFound,
                Message = "There is no movie with this Id"
            };
        }

        var movieDto = _mapper.Map<MovieDto>(movie);

        return movieDto;
    }

    public async Task<MovieDto> AddMovie(CreateMovieDto movieDto)
    {
        var movies = await _movieRepository.GetAllAsync();
        if (movies.Any(m => m.Title == movieDto.Title))
        {
            throw new ApiException
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "This movie already exists"
            };
        }
        var movie = _mapper.Map<Movie>(movieDto);
        IList<Genre> genres = new List<Genre>();
        var addedMovie = await _movieRepository.AddMovie(movie);
        var addedMovieDto = _mapper.Map<MovieDto>(addedMovie);

        return addedMovieDto;
    }

    public async Task<MovieDto> AddPhotoToMovieAsync(IFormFile photo, Guid movieId)
    {
        var movie = await _movieRepository.GetById(movieId);

        if (movie == null)
        {
            throw new ApiException
            {
                StatusCode = StatusCodes.Status404NotFound,
                Message = "No movie was found with this id"
            };
        }

        if (movie.PhotoId != null)
        {
            throw new ApiException
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "You can't change the photo"
            };
        }

        var photoResult = await _photoService.AddPhotoAsync(photo);
        var addedPhoto = await _photoRepository.AddPhotoAsync(photoResult, movieId);

        if (addedPhoto == null)
        {
            throw new ApiException
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "Problem ading photos"
            };
        }

        var updatedMovie = await _movieRepository.AddPhotoAsync(addedPhoto, movie);
        var movieDto = _mapper.Map<MovieDto>(updatedMovie);

        return movieDto;
    }
}
