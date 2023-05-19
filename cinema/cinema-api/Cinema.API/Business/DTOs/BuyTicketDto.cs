using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class BuyTicketDto
    {
        public Guid ShowingId { get; set; }

        public Guid SeatId { get; set; }
    }
}
