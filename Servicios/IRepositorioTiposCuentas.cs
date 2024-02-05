namespace ManejoPresupuesto;

public interface IRepositorioTiposCuentas
{
    Task Crear(TipoCuenta tipoCuenta);
    Task<bool> Existe(string nombre, int usuarioId);

    Task<IEnumerable<TipoCuenta>> Obtener(int usuarioId);

    Task Actualizar(TipoCuenta tipoCuenta);

    Task<TipoCuenta> ObtenerPorId(int id, int usuarioId);
    Task Borrar(int id);
    Task Ordenar(IEnumerable<TipoCuenta> tiposCuentasOrdenados);
}
