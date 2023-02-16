using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ufynd.Task2.Domain.Entities;

namespace Ufynd.Task2.Infrastructure.Persistence.Configurations
{
    public class AutoProcessingConfiguration : IEntityTypeConfiguration<AutoProcessing>
    {
        public void Configure(EntityTypeBuilder<AutoProcessing> builder)
        {
            builder.ToTable("AutoProcessing");
            builder.Property(a => a.Email).IsRequired();
            builder.Property(a => a.FileAddress).IsRequired();
            builder.Property(a => a.SendTime).IsRequired();
            builder.HasIndex(a => new { a.SendTime, a.Email });
        }
    }
}
