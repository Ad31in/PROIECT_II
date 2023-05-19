using DataLogic.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Entities
{
    public class Seat
    {
        public Guid Id { get; set; }

        public Guid RoomId { get; set; }

        public int Row { get; set; }

        public int Number { get; set; }

        public SeatState State { get; set; }

        public Room Room { get; set; }

        public ICollection<Ticket>? Tickets { get; set; }
    }

    public class SeatConfiguration : IEntityTypeConfiguration<Seat>
    {
        public void Configure(EntityTypeBuilder<Seat> configuration)
        {
            configuration.HasOne(_ => _.Room)
                .WithMany(_ => _.Seats)
                .HasForeignKey(_ => _.RoomId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
