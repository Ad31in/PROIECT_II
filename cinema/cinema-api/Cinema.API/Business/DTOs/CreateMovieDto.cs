using DataLogic.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class CreateMovieDto
    {
        [Required] public string Title { get; set; }

        [Required] public string Description { get; set; }

        [Required] public int DurationInHours { get; set; }

        [Required] public int DurationInMinutes { get; set; }

        [Required] public ICollection<GenreDto> Genres { get; set; }
    }
}
