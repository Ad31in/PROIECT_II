using DataLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Interfaces
{
    public interface ITicketRepository
    {
        Task<Ticket?> GetTicketByIdAsync(Guid ticketId, User user);

        Task<ICollection<Ticket>> GetAllAsync(User user);

        Task<Ticket?> AddTicketAsync(Ticket ticket, User user, Showing showing, Seat seat);
    }
}
