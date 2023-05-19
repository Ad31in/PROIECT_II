using DataLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class CreateShowingDto
    {
        public Guid MovieId { get; set; }

        public Guid RoomId { get; set; }

        public DateTime StartDate { get; set; }

        public double Price { get; set; }
    }
}
