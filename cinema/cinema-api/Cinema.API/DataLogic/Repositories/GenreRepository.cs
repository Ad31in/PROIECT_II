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
    public class GenreRepository : IGenreRepository
    {
        private readonly DataContext _dataContext;

        public GenreRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ICollection<Genre>> GetAllAsync()
        {
            return await _dataContext.Genres.ToListAsync();
        }

        public async Task AddGenres(IList<Genre> genres, Movie movie)
        {
            await _dataContext.Genres.AddRangeAsync(genres);
            await _dataContext.SaveChangesAsync();
        }
    }
}
