using DataLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class RoomDto
    {
        public Guid Id { get; set; }

        public int Number { get; set; }

        public int Capacity { get; set; }

        public ICollection<ShowingBriefDto>? Showings { get; set; }
    }
}
