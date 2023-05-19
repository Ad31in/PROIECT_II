using DataLogic.Data;
using DataLogic.Entities;
using DataLogic.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly DataContext _dataContext;

        public TicketRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Ticket> AddTicketAsync(Ticket ticket, User user, Showing showing, Seat seat)
        {
            await _dataContext.Tickets.AddAsync(ticket);
            user.Tickets.Add(ticket);
            showing.Tickets.Add(ticket);
            seat.Tickets.Add(ticket);
            seat.State = Enums.SeatState.Available;
            var result = await _dataContext.SaveChangesAsync() > 0;

            if (!result)
            {
                return null;
            }

            return ticket;
        }

        public async Task<ICollection<Ticket>> GetAllAsync(User user)
        {
            return await _dataContext.Tickets
                .Include(t => t.Showing)
                .Include(t => t.Seat)
                .Include(t => t.User)
                .Include(t => t.Showing.Room)
                .Include(t => t.Showing.Movie)
                .Where(t => t.UserId == user.Id).ToListAsync();
        }

        public async Task<Ticket?> GetTicketByIdAsync(Guid ticketId, User user)
        {
            return await _dataContext.Tickets
                .Include(t => t.Showing)
                .Include(t => t.Seat)
                .Include(t => t.User)
                .Include(t => t.Showing.Room)
                .Include(t => t.Showing.Movie)
                .FirstOrDefaultAsync(t => t.UserId == user.Id && t.Id == ticketId);
        }
    }
}
