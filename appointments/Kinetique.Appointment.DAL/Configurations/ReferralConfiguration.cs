using Kinetique.Appointment.Model;
using Kinetique.Shared.Model.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kinetique.Appointment.DAL.Configurations;

public class ReferralConfiguration : IEntityTypeConfiguration<Referral>
{
    public void Configure(EntityTypeBuilder<Referral> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Pesel)
            .HasConversion(x => x.Value, x => new Pesel(x));
    }
}