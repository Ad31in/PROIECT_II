using Business.DTOs;
using DataLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface ITicketService
    {
        Task<TicketDto> BuyTicketAsync(ClaimsPrincipal user, BuyTicketDto ticketDto);

        Task<TicketDto> ReserveTicketAsync(ClaimsPrincipal user, BuyTicketDto ticketDto);

        Task<TicketDto> GetTicketByIdAsync(Guid ticketId, ClaimsPrincipal user);

        Task<ICollection<TicketDto>> GetTicketsAsync(ClaimsPrincipal user);
    }
}
