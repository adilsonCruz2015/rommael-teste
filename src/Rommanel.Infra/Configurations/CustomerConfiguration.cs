using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rommanel.Core.Entities;


namespace Rommanel.Infra.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable(nameof(Customer));
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(250);

            builder.Property(x => x.Document)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(14);

            builder.Property(x => x.BirthDate)
                .IsRequired()
                .HasColumnType("datetime")
                .HasMaxLength(23);

            builder.Property(x => x.Phone)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(20);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(100);

            builder.OwnsOne(e => e.Address, address =>
            {
                address.Property(x => x.ZipCode)
                        .HasColumnName("ZipCode")
                        .HasColumnType("varchar")
                        .HasMaxLength(10)
                        .IsRequired();

                address.Property(x => x.Street)
                        .HasColumnType("varchar")
                        .HasMaxLength(350)
                        .IsRequired();

                address.Property(x => x.Number)
                    .HasColumnName("Number")
                    .HasColumnType("varchar")
                    .HasMaxLength(10)
                    .IsRequired();

                address.Property(x => x.Neighborhood)
                    .HasColumnName("Neighborhood")
                    .HasColumnType("varchar")
                    .HasMaxLength(350)
                    .IsRequired();

                address.Property(x => x.City)
                    .HasColumnName("City")
                    .HasColumnType("varchar")
                    .HasMaxLength(150)
                    .IsRequired();

                address.Property(x => x.State)
                    .HasColumnName("State")
                    .HasColumnType("varchar")
                    .HasMaxLength(150)
                    .IsRequired();
            });

            builder.Property(x => x.TaxExempt)
                .HasColumnType("bit")
                .HasMaxLength(20);

            builder.Property(x => x.StateRegistration)
                .HasColumnType("nvarchar")
                .HasMaxLength(20);
        }
    }
}
