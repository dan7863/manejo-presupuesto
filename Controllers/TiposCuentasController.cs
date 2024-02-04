namespace ManejoPresupuesto.Controllers;
using Microsoft.AspNetCore.Mvc;
using ManejoPresupuesto.Models;
using Microsoft.Data.SqlClient;
using Dapper;

public class TiposCuentasController : Controller
{
    // private readonly string connectionString;
    private readonly ILogger<HomeController> _logger;
    private readonly IRepositorioTiposCuentas repositorioTiposCuentas;

    public TiposCuentasController(// IConfiguration configuration, 
    ILogger<HomeController> logger,
    IRepositorioTiposCuentas repositorioTiposCuentas){
        // connectionString = configuration.GetConnectionString("DefaultConnection");
         _logger = logger;
         this.repositorioTiposCuentas = repositorioTiposCuentas;
    }

    public IActionResult Crear()
    {
        // using(var connection = new SqlConnection(connectionString))
        // {
        //     var query = connection.Query("SELECT 1").FirstOrDefault();
        //     Console.WriteLine(query);
            
        // }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Crear(TipoCuenta tipoCuenta){

        if(!ModelState.IsValid){
            return View(tipoCuenta);
        }

        tipoCuenta.UsuarioId = 1;

        var yaExisteTipoCuenta = await repositorioTiposCuentas.Existe(tipoCuenta.Nombre, tipoCuenta.UsuarioId);
        
        if(yaExisteTipoCuenta){
            ModelState.AddModelError(nameof(tipoCuenta.Nombre), 
            $"El nombre {tipoCuenta.Nombre} ya existe.");

            return View(tipoCuenta);
        }

        await repositorioTiposCuentas.Crear(tipoCuenta);

        return View();
    }

}
