using Business.DTOs;
using DataLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomDto>> GetRoomsAsync();

        Task<RoomDetailsDto> GetRoomByIdAsync(Guid id);

        Task<RoomDetailsDto> GetRoomByIdWithShowingsAsync(Guid id, Guid showingId);

        Task SetRoom(SetRoomDto roomSetup);
    }
}
