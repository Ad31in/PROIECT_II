using DataLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs;

public class TicketDto
{
    public Guid Id { get; set; }

    public ShowingDto Showing { get; set; }

    public SeatDto Seat { get; set; }

    public UserBriefDto User { get; set; }
}
