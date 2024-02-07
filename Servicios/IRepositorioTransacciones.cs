namespace ManejoPresupuesto.Servicios;

public interface IRepositorioTransacciones
{
    Task Crear(Transaccion transaccion);
    Task<Transaccion> ObtenerPorId(int id, int usuarioId);
}
