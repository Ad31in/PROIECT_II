using AutoMapper;
using Business.DTOs;
using Business.Interfaces;
using DataLogic.Entities;
using DataLogic.Enums;
using DataLogic.Interfaces;
using DatingApp.Common.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;

        public RoomService(IRoomRepository roomRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoomDto>> GetRoomsAsync()
        {
            var rooms = await _roomRepository.GetRoomsAsync();
            var roomsDto = rooms.Select(r => _mapper.Map<RoomDto>(r));

            return roomsDto;
        }

        public async Task<RoomDetailsDto> GetRoomByIdAsync(Guid id)
        {
            var room = await _roomRepository.GetRoomById(id);
            var roomDto = _mapper.Map<RoomDetailsDto>(room);

            return roomDto;
        }

        public async Task<RoomDetailsDto> GetRoomByIdWithShowingsAsync(Guid id, Guid showingId)
        {
            var room = await _roomRepository.GetRoomByIdWithShowings(id);


            // Get the existing ticket seat IDs in the showing
            if (room.Showings.Any())
            {
                var showing = room.Showings.FirstOrDefault(s => s.Id == showingId);
                if (showing == null || !showing.Tickets.Any())
                {
                    var roomDto = _mapper.Map<RoomDetailsDto>(room);

                    return roomDto;
                }

                var tickets = showing.Tickets.ToList();


                // Mark the seats as reserved if their IDs exist in the reservedSeatIds collection
                foreach (var seat in room.Seats)
                {
                    if (tickets.Any(rs => rs.SeatId == seat.Id))
                    {
                        seat.State = SeatState.Reserved;
                    }
                }

            }
            var roomWithSeatStateDto = _mapper.Map<RoomDetailsDto>(room);

            return roomWithSeatStateDto;

        }

        public async Task SetRoom(SetRoomDto roomSetup)
        {
            var rooms = await _roomRepository.GetRoomsAsync();

            bool alreadyExists = rooms.Any(room => room.Number == roomSetup.Number);

            if (alreadyExists)
            {
                throw new ApiException
                {
                    StatusCode = StatusCodes.Status406NotAcceptable,
                    Message = "Room already exists"
                };
            }

            await _roomRepository.CreateRoomWithSeatsAsync(roomSetup.Capacity, roomSetup.Number);
        }
    }
}
