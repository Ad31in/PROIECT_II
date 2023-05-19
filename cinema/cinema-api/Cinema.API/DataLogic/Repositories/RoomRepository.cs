using DataLogic.Data;
using DataLogic.Entities;
using DataLogic.Enums;
using DataLogic.Interfaces;
using DatingApp.Common.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly DataContext _context;

        public RoomRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Room>> GetRoomsAsync()
        {
            var rooms = await _context.Rooms
                .Include(r => r.Showings)
                .ToListAsync();

            return rooms;
        }

        public async Task<Room> GetRoomById(Guid roomId)
        {
            var room = await _context.Rooms
                .Include(r => r.Seats)
                .FirstOrDefaultAsync(r => r.Id == roomId);

            if (room == null)
            {
                throw new ApiException
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "No room was found with this id"
                };
            }

            room.Seats = room.Seats.OrderBy(s => s.Row).ThenBy(s => s.Number).ToList();

            return room;
        }

        public async Task<Room> GetRoomByIdWithShowings(Guid roomId)
        {
            var room = await _context.Rooms
                .Include(r => r.Seats)
                .Include(r => r.Showings)
                .ThenInclude(s => s.Tickets)
                .FirstOrDefaultAsync(r => r.Id == roomId);

            if (room == null)
            {
                throw new ApiException
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "No room was found with this id"
                };
            }

            room.Seats = room.Seats.OrderBy(s => s.Row).ThenBy(s => s.Number).ToList();

            return room;
        }

        public async Task CreateRoomWithSeatsAsync(int noSeats, int roomNumber)
        {
            var room = new Room
            {
                Id = Guid.NewGuid(),
                Number = roomNumber,
                Capacity = noSeats,
                Seats = new List<Seat>()
            };

            // Assuming 10 seats per row
            int noRows = (int)Math.Ceiling((double)noSeats / 10);

            for (int i = 1; i <= noRows; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    if (( i - 1 ) * 10 + j > noSeats)
                    {
                        break;
                    }

                    var seat = new Seat
                    {
                        Id = Guid.NewGuid(),
                        RoomId = room.Id,
                        Row = i,
                        Number = j,
                        State = SeatState.Available
                    };

                    room.Seats.Add(seat);
                }
            }

            await _context.Rooms.AddAsync(room);
            await _context.Seats.AddRangeAsync(room.Seats);
            await _context.SaveChangesAsync();

        }
    }
}
