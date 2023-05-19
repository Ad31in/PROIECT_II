using AutoMapper;
using Business.DTOs;
using Business.Interfaces;
using Common.Extensions;
using DataLogic.Entities;
using DataLogic.Interfaces;
using DatingApp.Common.Errors;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserRepository _userRepository;
        private readonly IShowingRepository _showingRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;

        public TicketService(ITicketRepository ticketRepository, IMapper mapper, IUserRepository userRepository, IShowingRepository showingRepository, IRoomRepository roomRepository)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _showingRepository = showingRepository;
            _roomRepository = roomRepository;
        }

        public async Task<TicketDto> BuyTicketAsync(ClaimsPrincipal claimedUser, BuyTicketDto ticketDto)
        {
            var user = await _userRepository.GetUserByEmailAsync(claimedUser.GetEmail());

            if (user == null)
            {
                throw new ApiException
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "No user was found"
                };
            }

            var showing = await _showingRepository.GetShowingByIdAsync(ticketDto.ShowingId);

            if (showing == null)
            {
                throw new ApiException
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "No showing was found"
                };
            }

            var room = await _roomRepository.GetRoomById(showing.RoomId);

            if (room == null)
            {
                throw new ApiException
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "No room was found"
                };
            }

            var seat = room.Seats.FirstOrDefault(s => s.Id == ticketDto.SeatId);

            if (seat == null)
            {
                throw new ApiException
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "No seat was found"
                };
            }

            var isSeatOccupied = showing.Tickets.Any(t => t.SeatId == ticketDto.SeatId);

            if (isSeatOccupied)
            {
                throw new ApiException
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "This seat is occupied"
                };
            }

            var ticket = new Ticket
            {
                ShowingId = showing.Id,
                Showing = showing,
                SeatId = ticketDto.SeatId,
                Seat = seat,
                UserId = user.Id,
                User = user
            };

            var savedTicket = await _ticketRepository.AddTicketAsync(ticket, user, showing, seat);

            if (savedTicket == null)
            {
                throw new ApiException
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Problem buying the ticket"
                };
            }

            var resultTicketDto = _mapper.Map<TicketDto>(savedTicket);

            return resultTicketDto;
        }
        public Task<TicketDto> ReserveTicketAsync(ClaimsPrincipal user, BuyTicketDto ticketDto)
        {
            throw new NotImplementedException();
        }

        public async Task<TicketDto> GetTicketByIdAsync(Guid ticketId, ClaimsPrincipal claimedUser)
        {
            var user = await _userRepository.GetUserByEmailAsync(claimedUser.GetEmail());

            if (user == null)
            {
                throw new ApiException
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "No user was found"
                };
            }

            var ticket = await _ticketRepository.GetTicketByIdAsync(ticketId, user);

            if (ticket == null)
            {
                throw new ApiException
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "No ticket was found"
                };
            }

            var ticketDto = _mapper.Map<TicketDto>(ticket);

            return ticketDto;
        }

        public async Task<ICollection<TicketDto>> GetTicketsAsync(ClaimsPrincipal claimedUser)
        {
            var user = await _userRepository.GetUserByEmailAsync(claimedUser.GetEmail());

            if (user == null)
            {
                throw new ApiException
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "No user was found"
                };
            }

            var tickets = await _ticketRepository.GetAllAsync(user);
            var ticketsDto = _mapper.Map<IList<TicketDto>>(tickets);

            return ticketsDto;
        }

    }
}
