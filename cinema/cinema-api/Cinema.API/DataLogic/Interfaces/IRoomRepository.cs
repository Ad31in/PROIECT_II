using DataLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Interfaces
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetRoomsAsync();

        Task<Room> GetRoomById(Guid roomId);

        Task<Room> GetRoomByIdWithShowings(Guid roomId);

        Task CreateRoomWithSeatsAsync(int noSeats, int roomNumber);
    }
}
