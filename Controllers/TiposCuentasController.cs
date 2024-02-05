﻿namespace ManejoPresupuesto.Controllers;
using Microsoft.AspNetCore.Mvc;
using ManejoPresupuesto.Models;
using Microsoft.Data.SqlClient;
using Dapper;
using ManejoPresupuesto.Servicios;

public class TiposCuentasController : Controller
{
    // private readonly string connectionString;
    private readonly ILogger<HomeController> _logger;
    private readonly IRepositorioTiposCuentas repositorioTiposCuentas;
    private readonly IServicioUsuarios servicioUsuarios;

    public TiposCuentasController(// IConfiguration configuration, 
    ILogger<HomeController> logger,
    IRepositorioTiposCuentas repositorioTiposCuentas,
    IServicioUsuarios servicioUsuarios){
        // connectionString = configuration.GetConnectionString("DefaultConnection");
         _logger = logger;
         this.repositorioTiposCuentas = repositorioTiposCuentas;
         this.servicioUsuarios = servicioUsuarios;
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

    public async Task<IActionResult> Index(){
        var usuarioId = servicioUsuarios.ObtenerUsuarioId();
        var tiposCuentas = await repositorioTiposCuentas.Obtener(usuarioId);
        return View(tiposCuentas);
    }


    [HttpPost]
    public async Task<IActionResult> Crear(TipoCuenta tipoCuenta){

        if(!ModelState.IsValid){
            return View(tipoCuenta);
        }

        tipoCuenta.UsuarioId = servicioUsuarios.ObtenerUsuarioId();

        var yaExisteTipoCuenta = await repositorioTiposCuentas.Existe(tipoCuenta.Nombre, tipoCuenta.UsuarioId);
        
        if(yaExisteTipoCuenta){
            ModelState.AddModelError(nameof(tipoCuenta.Nombre), 
            $"El nombre {tipoCuenta.Nombre} ya existe.");

            return View(tipoCuenta);
        }

        await repositorioTiposCuentas.Crear(tipoCuenta);

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<ActionResult> Editar(int id){
        var usuarioId = servicioUsuarios.ObtenerUsuarioId();
        var tipoCuenta = await repositorioTiposCuentas.ObtenerPorId(id, usuarioId);
        if(tipoCuenta is null){
            return RedirectToAction("NoEncontrado", "Home");
        }

        return View(tipoCuenta);
    }

    [HttpPost]
    public async Task<ActionResult> Editar(TipoCuenta tipoCuenta){
        var usuarioId = servicioUsuarios.ObtenerUsuarioId();
        var tipoCuentaExiste = await repositorioTiposCuentas.ObtenerPorId(tipoCuenta.Id, usuarioId);
        if(tipoCuentaExiste is null){
            return RedirectToAction("NoEncontrado", "Home");
        }

        await repositorioTiposCuentas.Actualizar(tipoCuenta);
        return RedirectToAction("Index");
    }   

    [HttpGet]
    public async Task<IActionResult> VerificarExisteTipoCuenta(string nombre){
        var usuarioId = servicioUsuarios.ObtenerUsuarioId();
        var yaExisteTipoCuenta = await repositorioTiposCuentas.Existe(nombre, usuarioId);
        Console.WriteLine(yaExisteTipoCuenta);
        if(yaExisteTipoCuenta){
            return Json($"El nombre {nombre} ya existe");
        }

        return Json(true);
    }

    public async Task<IActionResult> Borrar(int id){
        var usuarioId = servicioUsuarios.ObtenerUsuarioId();
        var tipoCuenta = await repositorioTiposCuentas.ObtenerPorId(id, usuarioId);

        if(tipoCuenta is null){
            return RedirectToAction("NoEncontrado", "Home");
        }

        return View(tipoCuenta);
    }

    [HttpPost]
    public async Task<IActionResult> BorrarTipoCuenta(int id){
 var usuarioId = servicioUsuarios.ObtenerUsuarioId();
        var tipoCuenta = await repositorioTiposCuentas.ObtenerPorId(id, usuarioId);

        if(tipoCuenta is null){
            return RedirectToAction("NoEncontrado", "Home");
        }

        await repositorioTiposCuentas.Borrar(id);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Ordenar([FromBody] int[] ids){
        var usuarioId = servicioUsuarios.ObtenerUsuarioId();
        var tiposCuentas = await repositorioTiposCuentas.Obtener(usuarioId);
        var idsTiposCuentas = tiposCuentas.Select(x => x.Id);

        var idsTiposCuentasNoPertenecenAlUsuario = ids.Except(idsTiposCuentas).ToList();
        
        if(idsTiposCuentasNoPertenecenAlUsuario.Count > 0){
            return Forbid();
        }

        var tiposCuentasOrdenados = ids.Select((valor, indice) => 
        new TipoCuenta() { Id = valor, Orden = indice +  1}).AsEnumerable();

        await repositorioTiposCuentas.Ordenar(tiposCuentasOrdenados);
        
        return Ok();
    }



}
