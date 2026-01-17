using Microsoft.Extensions.DependencyInjection;
using ServindAp.Application.Interfaces;
using ServindAp.Application.Interfaces.Repositories;
using ServindAp.Application.UseCases;

namespace ServindAp.Application.DependencyInjection
{
    /// <summary>
    /// Extensión para configurar la inyección de dependencias de la capa Application.
    /// Esta clase centraliza la configuración de todos los UseCases y servicios.
    /// </summary>
    public static class ApplicationServiceCollectionExtensions
    {
        /// <summary>
        /// Registra los servicios de la aplicación en el contenedor de DI.
        /// Debe ser llamado desde Program.cs o el punto de entrada de la aplicación.
        /// </summary>
        /// <param name="services">IServiceCollection del contenedor</param>
        /// <returns>El IServiceCollection para encadenar llamadas</returns>
        /// <example>
        /// En Program.cs o equivalente:
        /// services.AddApplicationServices();
        /// </example>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Registrar UseCases como Scoped
            // Cada solicitud obtiene su propia instancia
            services.AddScoped<CrearPrestamoUseCase>();
            services.AddScoped<ListarPrestamosUseCase>();
            services.AddScoped<ObtenerPrestamoUseCase>();
            services.AddScoped<RegistrarDevolucionUseCase>();
            services.AddScoped<ListarHerramientasUseCase>();

            // Registrar el servicio centralizado de aplicación
            services.AddScoped<IApplicationService, ApplicationService>();

            return services;
        }
    }

    /// <summary>
    /// Implementación de IApplicationService que actúa como punto de acceso único
    /// a todos los UseCases de la aplicación.
    /// 
    /// Este patrón actúa como Facade, simplificando el acceso a los casos de uso
    /// y centralizando la creación de dependencias.
    /// </summary>
    public class ApplicationService : IApplicationService
    {
        public CrearPrestamoUseCase CrearPrestamo { get; }
        public ListarPrestamosUseCase ListarPrestamos { get; }
        public ObtenerPrestamoUseCase ObtenerPrestamo { get; }
        public RegistrarDevolucionUseCase RegistrarDevolucion { get; }
        public ListarHerramientasUseCase ListarHerramientas { get; }

        /// <summary>
        /// Constructor que recibe todos los UseCases.
        /// Las dependencias son inyectadas por el contenedor de DI.
        /// </summary>
        public ApplicationService(
            CrearPrestamoUseCase crearPrestamo,
            ListarPrestamosUseCase listarPrestamos,
            ObtenerPrestamoUseCase obtenerPrestamo,
            RegistrarDevolucionUseCase registrarDevolucion,
            ListarHerramientasUseCase listarHerramientas)
        {
            CrearPrestamo = crearPrestamo ?? throw new ArgumentNullException(nameof(crearPrestamo));
            ListarPrestamos = listarPrestamos ?? throw new ArgumentNullException(nameof(listarPrestamos));
            ObtenerPrestamo = obtenerPrestamo ?? throw new ArgumentNullException(nameof(obtenerPrestamo));
            RegistrarDevolucion = registrarDevolucion ?? throw new ArgumentNullException(nameof(registrarDevolucion));
            ListarHerramientas = listarHerramientas ?? throw new ArgumentNullException(nameof(listarHerramientas));
        }
    }
}
