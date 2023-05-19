using DataLogic.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Interfaces
{
    public interface IGenreRepository
    {
        Task<ICollection<Genre>> GetAllAsync();

        Task AddGenres(IList<Genre> genres, Movie movie);
    }
}
