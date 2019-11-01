using CleanArchitecture.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Config
{
    public class UrlStatusHistoryConfiguration : IEntityTypeConfiguration<UrlStatusHistory>
    {
        public void Configure(EntityTypeBuilder<UrlStatusHistory> builder)
        {
            builder.Property(ush => ush.RequestDateUtc)
                .IsRequired();
            builder.Property(ush => ush.Id)
                .IsRequired();
            builder.Property(ush => ush.StatusCode)
                .IsRequired();
            builder.Property(ush => ush.Uri)
                .HasMaxLength(1024)
                .IsRequired();
        }
    }
}
