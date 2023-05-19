using Business.DTOs;
using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }


        /// <summary>
        /// Gets all Rooms
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetRooms()
        {
            var rooms = await _roomService.GetRoomsAsync();

            return Ok(rooms);
        }

        /// <summary>
        /// Get a room based on its ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{roomId}")]
        public async Task<IActionResult> GetRoomById(Guid roomId)
        {
            var rooms = await _roomService.GetRoomByIdAsync(roomId);
            return Ok(rooms);
        }

        [HttpGet("{roomId}/{showingId}")]
        public async Task<IActionResult> GetRoomByIdAndShowingId(Guid roomId, Guid showingId)
        {
            var rooms = await _roomService.GetRoomByIdWithShowingsAsync(roomId, showingId);
            return Ok(rooms);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> SetRoom(SetRoomDto roomSetup)
        {
            await _roomService.SetRoom(roomSetup);
            return Ok();
        }
    }
}
