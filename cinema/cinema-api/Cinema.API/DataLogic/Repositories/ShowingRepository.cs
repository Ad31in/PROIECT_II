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
    public class ShowingRepository : IShowingRepository
    {
        private readonly DataContext _dataContext;

        public ShowingRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        async Task<Showing?> IShowingRepository.AddShowingAsync(Showing showing)
        {
            _dataContext.Showings.Add(showing);
            bool result = await _dataContext.SaveChangesAsync() > 0;

            if (result)
            {
                return showing;
            }

            return null;
        }

        async Task<Showing> IShowingRepository.GetShowingByIdAsync(Guid showingId)
        {
            return await _dataContext.Showings
                .Include(s => s.Room)
                .Include(s => s.Movie)
                .Include(s => s.Tickets)
                .FirstAsync(s => s.Id == showingId);
        }

        async Task<IEnumerable<Showing>> IShowingRepository.GetShowingsByMovieAsync(Guid movieId)
        {
            return await _dataContext.Showings
                .Include(s => s.Room)
                .Include(s => s.Movie)
                .Where(s => s.MovieId == movieId)
                .ToListAsync();
        }

        public async Task DeleteShowingsBeforeDateAsync(DateTime date)
        {
            var showingsToDelete = await _dataContext.Showings
                .Where(showing => showing.StartDate < date)
                .ToListAsync();

            if (showingsToDelete.Any())
            {
                _dataContext.Showings.RemoveRange(showingsToDelete);
                await _dataContext.SaveChangesAsync();
            }
        }
    }
}
