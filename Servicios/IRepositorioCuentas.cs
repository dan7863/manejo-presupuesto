using ManejoPresupuesto.Models;

namespace ManejoPresupuesto;

public interface IRepositorioCuentas
{
    Task Crear(Cuenta cuenta);
    Task<IEnumerable<Cuenta>> Buscar(int usuarioId);
    Task<Cuenta> ObtenerPorId(int id, int usuarioId);
    Task Actualizar(CuentaCreacionViewModel cuenta);
    Task Borrar(int id);
}
