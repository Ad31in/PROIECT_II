using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Entities
{
    public class Photo
    {
        public Guid Id { get; set; }

        public Guid MovieId { get; set; }

        public string Url { get; set; }

        public string? PublicId { get; set; }

        public Movie Movie { get; set; }
    }
    public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> configuration)
        {
            configuration.HasOne(_ => _.Movie)
                .WithOne(_ => _.Photo)
                .HasForeignKey<Photo>(_ => _.MovieId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
