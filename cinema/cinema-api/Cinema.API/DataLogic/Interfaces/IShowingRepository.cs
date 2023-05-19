using DataLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Interfaces
{
    public interface IShowingRepository
    {
        Task<IEnumerable<Showing>> GetShowingsByMovieAsync(Guid movieId);

        Task<Showing> GetShowingByIdAsync(Guid showingId);

        Task<Showing?> AddShowingAsync(Showing showing);

        Task DeleteShowingsBeforeDateAsync(DateTime date);
    }
}
