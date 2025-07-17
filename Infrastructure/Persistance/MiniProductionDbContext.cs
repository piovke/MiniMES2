using Microsoft.EntityFrameworkCore;
using Domain.Models;


namespace Infrastructure.Persistance;

public class MiniProductionDbContext : DbContext
{
    public MiniProductionDbContext(DbContextOptions options) : base(options) { }

    public virtual DbSet<Machine> Machines { get; set; } = null!;
    public virtual DbSet<Product> Products { get; set; } = null!;
    public virtual DbSet<Order> Orders { get; set; } = null!;
    public virtual DbSet<Domain.Models.Process> Processes { get; set; } = null!;
    public virtual DbSet<ProcessParameters> ProcessParameters { get; set; } = null!;
    public virtual DbSet<Domain.Models.Parameter> Parameters { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Machine>(entity =>
        {
            entity.ToTable("Machines");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.Description).HasMaxLength(100);

            entity.Property(e => e.Name).HasMaxLength(50);
        });
        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Orders");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e=>e.Code).HasMaxLength(50);

            entity.HasOne(d => d.Machine)
                .WithMany(p => p.Orders)
                .HasForeignKey(d => d.MachineId);

            entity.HasOne(d => d.Product)
                .WithMany(p => p.Orders)
                .HasForeignKey(d => d.ProductId);
        });
        modelBuilder.Entity<Domain.Models.Parameter>(entity =>
        {
            entity.ToTable("Parameters");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.Name).HasMaxLength(50);

            entity.Property(e => e.Unit).HasMaxLength(50);
        });
        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Products");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(100);
        });
        modelBuilder.Entity<Domain.Models.Process>(entity =>
        {
            entity.ToTable("Processes");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.HasOne(d => d.Order)
                .WithMany(p=>p.Processes)
                .HasForeignKey(d => d.OrderId);
        });
        modelBuilder.Entity<ProcessParameters>(entity =>
        {
            entity.ToTable("ProcessParameters");
            entity.HasOne(d => d.Process)
                .WithMany(p => p.ProcessParameters)
                .HasForeignKey(d => d.ProcessId);
            entity.HasOne(d=>d.Parameter)
                .WithMany(p => p.ProcessParameters)
                .HasForeignKey(d => d.ParameterId);
        });
    }
}