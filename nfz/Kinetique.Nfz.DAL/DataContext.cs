using Kinetique.Nfz.Model;
using Kinetique.Shared.Model;
using Kinetique.Shared.Model.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Kinetique.Nfz.DAL;

public class DataContext(DbContextOptions options, IClock clock) : DbContext(options)
{
    public DbSet<SettlementProcedure> SettlementProcedures { get; set; }
    public DbSet<StatisticProcedure> StatisticProcedures { get; set; }
    public DbSet<PatientProcedure> PatientProcedures { get; set; }
    public DbSet<Referral> Referrals { get; set; }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        modelBuilder.Entity<StatisticProcedure>()
            .HasIndex(x => x.Treatment).IsUnique();
        
        modelBuilder.Entity<StatisticProcedure>()
            .HasOne(sp => sp.SettlementProcedure)
            .WithOne(s => s.StatisticProcedure)
            .HasForeignKey<SettlementProcedure>(s => s.StatisticProcedureId);

        
        base.OnModelCreating(modelBuilder);
    }
    
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        var entries = ChangeTracker.Entries().Where(x => x is { Entity: BaseModel, State: EntityState.Added or EntityState.Modified });
        foreach (var entry in entries) { ((BaseModel)entry.Entity).LastUpdate = clock.GetNow(); }
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries().Where(x => x is {Entity: BaseModel, State: EntityState.Added or EntityState.Modified});
        foreach (var entry in entries) { ((BaseModel)entry.Entity).LastUpdate = clock.GetNow(); }
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}
