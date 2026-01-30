using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ServindAp.Application.Interfaces.Repositories;
using ServindAp.Infrastructure.Data;
using ServindAp.Infrastructure.Persistence;

namespace ServindAp.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ServindApDbContext>(options =>
                options.UseSqlite(connectionString));

            services.AddScoped<IPrestamoRepository, PrestamoRepository>();
            services.AddScoped<IHerramientaRepository, HerramientaRepository>();
            services.AddScoped<IPrestamoHerramientaRepository, PrestamoHerramientaRepository>();
            services.AddScoped<IHistorialRepository, HistorialRepository>();

            return services;
        }

        public static void InitializeDatabase(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ServindApDbContext>();
            context.Database.EnsureCreated();
            
            // Crear triggers de historial automático
            CreateHistorialTriggers(context);
        }
        
        /// <summary>
        /// Crea los triggers de SQLite para el historial automático de préstamos y devoluciones.
        /// Se ejecuta después de EnsureCreated() para garantizar que la BD esté lista.
        /// </summary>
        private static void CreateHistorialTriggers(ServindApDbContext context)
        {
            // Trigger 1: Registrar PRESTAMO cuando se inserta en Prestamo_Herramienta
            var triggerPrestamo = @"
                CREATE TRIGGER IF NOT EXISTS trg_historial_prestamo
                AFTER INSERT ON Prestamo_Herramienta
                FOR EACH ROW
                BEGIN
                    INSERT INTO Historial (
                        prestamo_id, 
                        herramienta_id, 
                        cantidad, 
                        tipo_evento, 
                        fecha_evento
                    ) VALUES (
                        NEW.prestamo_id,
                        NEW.herramienta_id,
                        NEW.cantidad,
                        'PRESTAMO',
                        datetime('now', 'localtime')
                    );
                END;
            ";
            
            // Trigger 2: Registrar DEVOLUCION cuando la cantidad disminuye
            var triggerDevolucion = @"
                CREATE TRIGGER IF NOT EXISTS trg_historial_devolucion
                AFTER UPDATE ON Prestamo_Herramienta
                FOR EACH ROW
                WHEN NEW.cantidad < OLD.cantidad
                BEGIN
                    INSERT INTO Historial (
                        prestamo_id, 
                        herramienta_id, 
                        cantidad, 
                        tipo_evento, 
                        fecha_evento
                    ) VALUES (
                        NEW.prestamo_id,
                        NEW.herramienta_id,
                        OLD.cantidad - NEW.cantidad,  -- Cantidad devuelta
                        CASE 
                            WHEN EXISTS (
                                SELECT 1 FROM Prestamos p 
                                WHERE p.id = NEW.prestamo_id 
                                AND p.estado_prestamo = 3  -- Devuelto con Defectos
                            ) THEN 'DEVOLUCION_CON_DEFECTOS'
                            ELSE 'DEVOLUCION'
                        END,
                        datetime('now', 'localtime')
                    );
                END;
            ";
            
            // Trigger 3: Registrar CANCELACION cuando se elimina un préstamo activo
            var triggerCancelacion = @"
                CREATE TRIGGER IF NOT EXISTS trg_historial_cancelacion
                BEFORE DELETE ON Prestamo_Herramienta
                FOR EACH ROW
                WHEN OLD.cantidad > 0
                BEGIN
                    INSERT INTO Historial (
                        prestamo_id, 
                        herramienta_id, 
                        cantidad, 
                        tipo_evento, 
                        fecha_evento,
                        observaciones
                    ) VALUES (
                        OLD.prestamo_id,
                        OLD.herramienta_id,
                        OLD.cantidad,
                        'CANCELACION',
                        datetime('now', 'localtime'),
                        'Préstamo eliminado antes de completar devolución'
                    );
                END;
            ";
            
            // Ejecutar los triggers
            context.Database.ExecuteSqlRaw(triggerPrestamo);
            context.Database.ExecuteSqlRaw(triggerDevolucion);
            context.Database.ExecuteSqlRaw(triggerCancelacion);
        }
    }
}
