namespace ServindAp.Application.Interfaces
{
    public interface IApplicationService
    {
        /// <summary>
        /// Acceso al UseCase de crear préstamos.
        /// </summary>
        UseCases.CrearPrestamoUseCase CrearPrestamo { get; }

        /// <summary>
        /// Acceso al UseCase de listar préstamos.
        /// </summary>
        UseCases.ListarPrestamosUseCase ListarPrestamos { get; }

        /// <summary>
        /// Acceso al UseCase de obtener un préstamo específico.
        /// </summary>
        UseCases.ObtenerPrestamoUseCase ObtenerPrestamo { get; }

        /// <summary>
        /// Acceso al UseCase de registrar devoluciones.
        /// </summary>
        UseCases.RegistrarDevolucionUseCase RegistrarDevolucion { get; }

        /// <summary>
        /// Acceso al UseCase de registrar devoluciones parciales.
        /// </summary>
        UseCases.RegistrarDevolucionParcialUseCase RegistrarDevolucionParcial { get; }

        /// <summary>
        /// Acceso al UseCase de actualizar préstamos.
        /// </summary>
        UseCases.ActualizarPrestamoUseCase ActualizarPrestamo { get; }

        /// <summary>
        /// Acceso al UseCase de eliminar préstamos.
        /// </summary>
        UseCases.EliminarPrestamoUseCase EliminarPrestamo { get; }

        /// <summary>
        /// Acceso al UseCase de listar herramientas.
        /// </summary>
        UseCases.ListarHerramientasUseCase ListarHerramientas { get; }

        /// <summary>
        /// Acceso al UseCase de crear herramientas.
        /// </summary>
        UseCases.CrearHerramientaUseCase CrearHerramienta { get; }

        /// <summary>
        /// Acceso al UseCase de actualizar herramientas.
        /// </summary>
        UseCases.ActualizarHerramientaUseCase ActualizarHerramienta { get; }

        /// <summary>
        /// Acceso al UseCase de obtener una herramienta específica.
        /// </summary>
        UseCases.ObtenerHerramientaUseCase ObtenerHerramienta { get; }

        /// <summary>
        /// Acceso al UseCase de eliminar herramientas.
        /// </summary>
        UseCases.EliminarHerramientaUseCase EliminarHerramienta { get; }

        /// <summary>
        /// Acceso al UseCase de listar historial.
        /// </summary>
        UseCases.ListarHistorialUseCase ListarHistorial { get; }
    }
}
