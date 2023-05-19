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
    public class Genre
    {
        public Guid Id { get; set; }

        public Genres Name { get; set; }

        public Guid MovieId { get; set; }

        public Movie Movie { get; set; }
    }
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> configuration)
        {
            configuration.HasOne(_ => _.Movie)
                .WithMany(_ => _.Genres)
                .HasForeignKey(_ => _.MovieId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}