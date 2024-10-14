using Kinetique.Appointment.Model;
using Kinetique.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace Kinetique.Appointment.DAL;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    { }
    
    public DbSet<Model.Appointment> Appointments { get; set; }
    
    public DbSet<AppointmentJournal> Journals { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        var entries = ChangeTracker.Entries().Where(x => x is { Entity: BaseModel, State: EntityState.Added or EntityState.Modified });
        foreach (var entry in entries) { ((BaseModel)entry.Entity).LastUpdate = DateTime.UtcNow; }
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries().Where(x => x is {Entity: BaseModel, State: EntityState.Added or EntityState.Modified});
        foreach (var entry in entries) { ((BaseModel)entry.Entity).LastUpdate = DateTime.UtcNow; }
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}