using AutoMapper;
using Business.DTOs;
using Business.Interfaces;
using DataLogic.Entities;
using DataLogic.Interfaces;
using DatingApp.Common.Errors;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class ShowingService : IShowingService
    {
        private readonly IMapper _mapper;
        private readonly IMovieRepository _movieRepository;
        private readonly IShowingRepository _showingRepository;
        private readonly IRoomRepository _roomRepository;

        public ShowingService(IMapper mapper, IShowingRepository showingRepository, IRoomRepository roomRepository, IMovieRepository movieRepository)
        {
            _mapper = mapper;
            _showingRepository = showingRepository;
            _roomRepository = roomRepository;
            _movieRepository = movieRepository;
        }

        public async Task<ShowingDto> AddShowingAsync(CreateShowingDto createShowingDto)
        {
            var movie = await _movieRepository.GetById(createShowingDto.MovieId);
            var room = await _roomRepository.GetRoomById(createShowingDto.RoomId);

            if (movie == null || room == null)
            {
                throw new ApiException
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Not found"
                };
            }

            DateTime endDate = createShowingDto.StartDate + movie.Duration;
            var showing = new Showing
            {
                MovieId = createShowingDto.MovieId,
                RoomId = createShowingDto.RoomId,
                StartDate = createShowingDto.StartDate,
                EndDate = endDate,
                Price = createShowingDto.Price,
                Movie = movie,
                Room = room
            };
            var createdShowing = await _showingRepository.AddShowingAsync(showing);

            if (createdShowing == null)
            {
                throw new ApiException
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "There was a problem in creating the showing"
                };
            }

            var showingDto = _mapper.Map<ShowingDto>(createdShowing);

            return showingDto;
        }

        public async Task<ICollection<ShowingDto>> GetShowingsByMovieId(Guid movieId)
        {
            var showings = await _showingRepository.GetShowingsByMovieAsync(movieId);
            var showingsDto = _mapper.Map<IList<ShowingDto>>(showings);

            return showingsDto;
        }

        public async Task<ShowingDto> GetShowingByIdAsync(Guid showingId)
        {
            var showing = await _showingRepository.GetShowingByIdAsync(showingId);

            if (showing == null)
            {
                throw new ApiException
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "No showing was found"
                };
            }

            var showingDto = _mapper.Map<ShowingDto>(showing);

            return showingDto;
        }

        public async Task ReloadShowingsAsync()
        {
            DateTime date = DateTime.UtcNow;

            await _showingRepository.DeleteShowingsBeforeDateAsync(date);
        }
    }
}
