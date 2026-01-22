using Microsoft.EntityFrameworkCore;
using ServindAp.Domain.Entities;
using ServindAp.Domain.Enums;

namespace ServindAp.Infrastructure.Data
{
    public class ServindApDbContext : DbContext
    {
        public DbSet<Prestamo> Prestamos { get; set; }
        public DbSet<Herramienta> Herramientas { get; set; }
        public DbSet<EstadoPrestamo> EstadosPrestamo { get; set; }
        public DbSet<PrestamoHerramienta> PrestamosHerramientas { get; set; }

        public ServindApDbContext(DbContextOptions<ServindApDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Prestamo>(entity =>
            {
                entity.ToTable("Prestamos");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Responsable).HasColumnName("responsable").IsRequired();
                entity.Property(e => e.FechaEntrega).HasColumnName("fecha_entrega").IsRequired();
                entity.Property(e => e.FechaDevolucion).HasColumnName("fecha_devolucion");
                entity.Property(e => e.Observaciones).HasColumnName("observaciones");
                
                entity.Property(e => e.Estado)
                    .HasColumnName("estado_prestamo")
                    .HasConversion<int>()
                    .IsRequired();

                entity.Ignore(e => e.Herramientas);
            });

            modelBuilder.Entity<EstadoPrestamo>(entity =>
            {
                entity.ToTable("estados_prestamo");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Nombre).HasColumnName("nombre").IsRequired();

                entity.HasData(
                    new EstadoPrestamo { Id = 1, Nombre = "Activo" },
                    new EstadoPrestamo { Id = 2, Nombre = "Devuelto" },
                    new EstadoPrestamo { Id = 3, Nombre = "Devuelto con Defectos" }
                );
            });

            modelBuilder.Entity<Herramienta>(entity =>
            {
                entity.ToTable("Herramientas");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Nombre).HasColumnName("nombre").IsRequired();
                entity.Property(e => e.Stock).HasColumnName("stock").IsRequired().HasDefaultValue(0);
                entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            });

            modelBuilder.Entity<PrestamoHerramienta>(entity =>
            {
                entity.ToTable("Prestamo_Herramienta");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.PrestamoId).HasColumnName("prestamo_id").IsRequired();
                entity.Property(e => e.HerramientaId).HasColumnName("herramienta_id").IsRequired();
                entity.Property(e => e.Cantidad).HasColumnName("cantidad").IsRequired();

                entity.HasOne(e => e.Prestamo)
                    .WithMany()
                    .HasForeignKey(e => e.PrestamoId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Herramienta)
                    .WithMany()
                    .HasForeignKey(e => e.HerramientaId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
