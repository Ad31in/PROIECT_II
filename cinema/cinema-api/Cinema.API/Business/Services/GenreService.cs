using AutoMapper;
using Business.DTOs;
using Business.Interfaces;
using DataLogic.Data;
using DataLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GenreService(IGenreRepository genreRepository, IMapper mapper)
        {
            _mapper = mapper;
            _genreRepository = genreRepository;
        }

        public async Task<ICollection<GenreDto>> GetGenresAsync()
        {
            var genres = await _genreRepository.GetAllAsync();
            var genresDto = _mapper.Map<IList<GenreDto>>(genres);

            return genresDto;
        }
    }
}
