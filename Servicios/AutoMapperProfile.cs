using AutoMapper;
using ManejoPresupuesto.Models;

namespace ManejoPresupuesto;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile(){
        CreateMap<Cuenta, CuentaCreacionViewModel>();
    }
}
