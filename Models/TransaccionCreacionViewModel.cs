using System.ComponentModel.DataAnnotations;
using ManejoPresupuesto.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ManejoPresupuesto;

public class TransaccionCreacionViewModel : Transaccion
{
    public IEnumerable<SelectListItem> Cuentas { get; set; }
    public IEnumerable<SelectListItem> Categorias { get; set; }
}
