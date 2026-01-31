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
        public DbSet<HistorialPrestamoHerramienta> HistorialPrestamosHerramientas { get; set; }

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
                
                // Columna temporal para manejar defectos en devoluciones parciales
                entity.Property(e => e.TieneDefectosTemp)
                    .HasColumnName("tiene_defectos_temp")
                    .HasDefaultValue(false);

                entity.HasOne(e => e.Prestamo)
                    .WithMany()
                    .HasForeignKey(e => e.PrestamoId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Herramienta)
                    .WithMany()
                    .HasForeignKey(e => e.HerramientaId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de tabla de Historial (append-only, gestionada por triggers)
            modelBuilder.Entity<HistorialPrestamoHerramienta>(entity =>
            {
                entity.ToTable("Historial");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.PrestamoId).HasColumnName("prestamo_id").IsRequired();
                entity.Property(e => e.HerramientaId).HasColumnName("herramienta_id").IsRequired();
                entity.Property(e => e.Cantidad).HasColumnName("cantidad").IsRequired();
                entity.Property(e => e.TipoEvento).HasColumnName("tipo_evento").IsRequired().HasMaxLength(20);
                entity.Property(e => e.FechaEvento).HasColumnName("fecha_evento").IsRequired();
                entity.Property(e => e.Observaciones).HasColumnName("observaciones");

                entity.HasOne(e => e.Prestamo)
                    .WithMany()
                    .HasForeignKey(e => e.PrestamoId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Herramienta)
                    .WithMany()
                    .HasForeignKey(e => e.HerramientaId)
                    .OnDelete(DeleteBehavior.Restrict);
                    
                // Constraint para validar tipo de evento
                entity.ToTable(t => t.HasCheckConstraint(
                    "CK_TipoEvento", 
                    "tipo_evento IN ('PRESTAMO', 'DEVOLUCION', 'DEVOLUCION_CON_DEFECTOS', 'CANCELACION')"
                ));
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
