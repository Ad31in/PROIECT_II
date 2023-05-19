using Business.DTOs;
using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost("buy")]
        public async Task<IActionResult> BuyTicket(BuyTicketDto ticketData)
        {
            var ticket = await _ticketService.BuyTicketAsync(User, ticketData);

            return Ok(ticket);
        }

        [HttpPost("reserve")]
        public async Task<IActionResult> ReserveTicket(BuyTicketDto ticketData)
        {
            return Ok(ticketData);
        }

        [HttpGet("{ticketId}")]
        public async Task<IActionResult> GetTicketbyId(Guid ticketId)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(ticketId, User);

            return Ok(ticket);
        }

        [HttpGet]
        public async Task<IActionResult> GetTickets()
        {
            var ticket = await _ticketService.GetTicketsAsync(User);

            return Ok(ticket);
        }
    }
}
