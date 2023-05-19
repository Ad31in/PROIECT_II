using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Entities
{
    public class Ticket
    {
        public Guid Id { get; set; }

        public Guid ShowingId { get; set; }

        public Guid SeatId { get; set; }

        public Guid UserId { get; set; }

        public Showing Showing { get; set; }

        public Seat Seat { get; set; }

        public User User { get; set; }
    }

    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> configuration)
        {
            configuration.HasOne(_ => _.Showing)
                .WithMany(_ => _.Tickets)
                .HasForeignKey(_ => _.ShowingId)
                .OnDelete(DeleteBehavior.Cascade);

            configuration.HasOne(_ => _.Seat)
                .WithMany(_ => _.Tickets)
                .HasForeignKey(_ => _.SeatId)
                .OnDelete(DeleteBehavior.Cascade);

            configuration.HasOne(_ => _.User)
                .WithMany(_ => _.Tickets)
                .HasForeignKey(_ => _.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
