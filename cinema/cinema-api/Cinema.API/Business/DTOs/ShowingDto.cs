using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class ShowingDto
    {
        public Guid Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public MovieBriefDto Movie { get; set; }

        public RoomDto Room { get; set; }

        public double Price { get; set; }
    }
}
