using Kinetique.Appointment.Model;
using Kinetique.Shared.Model.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kinetique.Appointment.DAL.Configurations;

public class AppointmentCycleConfiguration :  IEntityTypeConfiguration<AppointmentCycle>
{
    public void Configure(EntityTypeBuilder<AppointmentCycle> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.PatientPesel)
            .HasConversion(x => x.Value, x => new Pesel(x));
    }
}