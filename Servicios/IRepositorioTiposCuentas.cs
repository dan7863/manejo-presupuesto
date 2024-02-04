﻿namespace ManejoPresupuesto;

public interface IRepositorioTiposCuentas
{
    Task Crear(TipoCuenta tipoCuenta);
    Task<bool> Existe(string nombre, int usuarioId);
}
