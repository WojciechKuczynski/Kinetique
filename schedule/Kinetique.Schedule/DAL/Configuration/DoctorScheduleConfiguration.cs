using Kinetique.Schedule.Models;
using Kinetique.Schedule.Models.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kinetique.Schedule.DAL.Configuration;

public class DoctorScheduleConfiguration : IEntityTypeConfiguration<DoctorSchedule>
{
    public void Configure(EntityTypeBuilder<DoctorSchedule> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.StartDate)
            .HasConversion(x => x.Value, x => new Date(x));
        builder.Property(x => x.EndDate)
            .HasConversion(x => x.Value, x => new Date(x));
    }
}