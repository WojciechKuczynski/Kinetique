using Kinetique.Schedule.Models;
using Kinetique.Shared.Model;
using Kinetique.Shared.Model.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Kinetique.Schedule.DAL;

public class DataContext(DbContextOptions options, IClock clock) : DbContext(options)
{
    private readonly IClock _clock = clock;

    public DbSet<ScheduleBlocker> ScheduleBlockers { get; set; }
    
    public DbSet<DoctorSchedule> DoctorSchedules { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        modelBuilder.Entity<DoctorSchedule>()
            .HasMany(x => x.Slots)
            .WithOne(x => x.DoctorSchedule)
            .HasForeignKey(x => x.DoctorScheduleId);
        modelBuilder.Entity<DoctorSchedule>()
            .HasMany(x => x.Blockers)
            .WithOne(x => x.DoctorSchedule)
            .HasForeignKey(x => x.DoctorScheduleId);
        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        var entries = ChangeTracker.Entries().Where(x => x is { Entity: BaseModel, State: EntityState.Added or EntityState.Modified });
        foreach (var entry in entries) { ((BaseModel)entry.Entity).LastUpdate = _clock.GetNow(); }
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
    {
        var entries = ChangeTracker.Entries().Where(x => x is {Entity: BaseModel, State: EntityState.Added or EntityState.Modified});
        foreach (var entry in entries) { ((BaseModel)entry.Entity).LastUpdate = _clock.GetNow(); }
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
    
}