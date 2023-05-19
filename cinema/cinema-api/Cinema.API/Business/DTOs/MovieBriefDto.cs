using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class MovieBriefDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public TimeSpan Duration { get; set; }

        public string? PhotoUrl { get; set; }

        public ICollection<GenreDto>? Genres { get; set; }
    }
}
