using Business.DTOs;
using DataLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IShowingService
    {
        Task<ShowingDto> GetShowingByIdAsync(Guid id);

        Task<ICollection<ShowingDto>> GetShowingsByMovieId(Guid movieId);

        Task<ShowingDto> AddShowingAsync(CreateShowingDto createShowingDto);

        Task ReloadShowingsAsync();
    }
}
