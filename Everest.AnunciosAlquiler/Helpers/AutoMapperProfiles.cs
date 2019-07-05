using AutoMapper;
using Everest.Entities;
using Everest.ViewModels.Request;
using Everest.ViewModels.Response;

namespace Everest.AnunciosAlquiler.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UsuarioEntity, UsuarioResponse>();

            CreateMap<CreacionAnuncioRequest, AnuncioEntity>();
            CreateMap<CreacionAnuncioRequest, AnuncioDetalleEntity>();
            CreateMap<CreacionAnuncioRequest, UbicacionEntity>();
            CreateMap<CreacionAnuncioRequest, TipoPropiedadEntity>();

            CreateMap<AnuncioEntity, AnuncioResponse>();
            CreateMap<AnuncioDetalleEntity, AnuncioResponse>();
            CreateMap<UbicacionEntity, AnuncioResponse>();
            CreateMap<TipoPropiedadEntity, TipoPropiedadResponse>();
            CreateMap<ImagenEntity, ImagenResponse>();
            CreateMap<EvaluacionEntity, EvaluacionResponse>();
        }
    }
}
