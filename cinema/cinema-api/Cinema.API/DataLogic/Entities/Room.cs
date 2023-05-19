using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Entities
{
    public class Room
    {
        public Guid Id { get; set; }

        public int Number { get; set; }

        public int Capacity { get; set; }

        public ICollection<Seat> Seats { get; set; }

        public ICollection<Showing>? Showings { get; set; }
    }
}
