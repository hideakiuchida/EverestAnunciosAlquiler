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
            MappingRequests();
            MappingResponses();
        }

        private void MappingRequests()
        {
            CreateMap<CreacionAnuncioRequest, AnuncioEntity>();
            CreateMap<CreacionAnuncioRequest, AnuncioDetalleEntity>();
            CreateMap<CreacionAnuncioRequest, UbicacionEntity>();
            CreateMap<CreacionAnuncioRequest, TipoPropiedadEntity>();

            CreateMap<EdicionAnuncioRequest, AnuncioEntity>();
            CreateMap<EdicionAnuncioRequest, AnuncioDetalleEntity>();
            CreateMap<EdicionAnuncioRequest, UbicacionEntity>();
            CreateMap<EdicionAnuncioRequest, TipoPropiedadEntity>();

            CreateMap<CreacionEvaluacionRequest, EvaluacionEntity>();
            CreateMap<CreacionImagenRequest, ImagenEntity>();
        }

        private void MappingResponses()
        {
            CreateMap<UsuarioEntity, UsuarioResponse>();

            CreateMap<AnuncioEntity, AnuncioResponse>();
            CreateMap<TipoPropiedadEntity, TipoPropiedadResponse>();
            CreateMap<ImagenEntity, ImagenResponse>();
            CreateMap<EvaluacionEntity, EvaluacionResponse>();
        }
    }
}
