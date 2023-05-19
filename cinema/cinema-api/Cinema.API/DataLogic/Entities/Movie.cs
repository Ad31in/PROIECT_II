using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Entities
{
    public class Movie
    {
        public Guid Id { get; set; }

        public Guid? PhotoId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public TimeSpan Duration { get; set; }

        public Photo? Photo { get; set; }

        public ICollection<Genre>? Genres { get; set; }

        public ICollection<Showing>? Showings { get; set; }
    }

    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> configuration)
        {
            configuration.HasOne(_ => _.Photo)
                .WithOne(_ => _.Movie)
                .HasForeignKey<Movie>(_ => _.PhotoId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
