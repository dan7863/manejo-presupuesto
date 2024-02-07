using ManejoPresupuesto.Models;

namespace ManejoPresupuesto;

public interface IRepositorioCategorias
{
    Task Crear(Categoria categoria);
    Task<IEnumerable<Categoria>> Obtener(int usuarioId);
    Task<IEnumerable<Categoria>> Obtener(int usuarioId, TipoOperacion tipoOperacionId);
    Task<Categoria> ObtenerPorId(int id, int usuarioId);
    Task Actualizar(Categoria categoria);
    Task Borrar(int id);
}
