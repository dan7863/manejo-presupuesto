using ManejoPresupuesto.Models;

namespace ManejoPresupuesto;

public interface IRepositorioCuentas
{
    Task Crear(Cuenta cuenta);
    Task<IEnumerable<Cuenta>> Buscar(int usuarioId);
}
