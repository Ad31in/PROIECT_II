using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Entities
{
    public class Showing
    {
        public Guid Id { get; set; }

        public Guid MovieId { get; set; }

        public Guid RoomId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public double Price { get; set; }

        public Movie Movie { get; set; }

        public Room Room { get; set; }

        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }

    public class ShowingConfiguration : IEntityTypeConfiguration<Showing>
    {
        public void Configure(EntityTypeBuilder<Showing> configuration)
        {
            configuration.HasOne(_ => _.Room)
                .WithMany(_ => _.Showings)
                .HasForeignKey(_ => _.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            configuration.HasOne(_ => _.Movie)
                .WithMany(_ => _.Showings)
                .HasForeignKey(_ => _.MovieId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
