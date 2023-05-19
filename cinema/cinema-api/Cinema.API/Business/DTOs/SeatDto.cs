using DataLogic.Entities;
using DataLogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class SeatDto
    {
        public Guid Id { get; set; }

        public Guid RoomId { get; set; }

        public int Row { get; set; }

        public int Number { get; set; }

        public SeatState State { get; set; }
    }
}
