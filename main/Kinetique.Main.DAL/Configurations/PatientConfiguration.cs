using Kinetique.Main.Model;
using Kinetique.Main.Model.ValueObjects;
using Kinetique.Shared.Model.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kinetique.Main.DAL.Configurations;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Address)
            .HasConversion(x => x.Value, x => new Address(x));
        builder.Property(x => x.Pesel)
            .HasConversion(x => x.Value, x => new Pesel(x));
        builder.Property(x => x.PhoneNumber)
            .HasConversion(x => x.ToString(), x => new PhoneNumber(x));
    }
}