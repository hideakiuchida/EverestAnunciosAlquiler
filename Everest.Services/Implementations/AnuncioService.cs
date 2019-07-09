using AutoMapper;
using Everest.Entities;
using Everest.Repository.Interfaces;
using Everest.Services.Interfaces;
using Everest.ViewModels;
using Everest.ViewModels.Request;
using Everest.ViewModels.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Everest.Services.Implementations
{
    public class AnuncioService : IAnuncioService
    {
        private readonly IMapper _mapper;
        private readonly IAnuncioRepository _anuncioRepository;
        private readonly IAnuncioDetalleRepository _anuncioDetalleRepository;
        private readonly IUbicacionRepository _ubicacionRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IEvaluacionRepository _evaluacionRepository;
        private readonly IImagenRepository _imagenRepository;
        private readonly ITipoPropiedadRepository _tipoPropiedadRepository;

        public AnuncioService(IAnuncioRepository anuncioRepository, IAnuncioDetalleRepository anuncioDetalleRepository, IUsuarioRepository usuarioRepository, ITipoPropiedadRepository tipoPropiedadRepository,
            IUbicacionRepository ubicacionRepository, IEvaluacionRepository evaluacionRepository, IImagenRepository imagenRepository, IMapper mapper)
        {
            _anuncioRepository = anuncioRepository;
            _anuncioDetalleRepository = anuncioDetalleRepository;
            _usuarioRepository = usuarioRepository;
            _ubicacionRepository = ubicacionRepository;
            _imagenRepository = imagenRepository;
            _evaluacionRepository = evaluacionRepository;
            _tipoPropiedadRepository = tipoPropiedadRepository;
            _mapper = mapper;
        }

        public async Task<BaseServiceResponse<IEnumerable<AnuncioResponse>>> ConsultarPorUsuarioAsync(int idUsuario)
        {
            BaseServiceResponse<IEnumerable<AnuncioResponse>> response = new BaseServiceResponse<IEnumerable<AnuncioResponse>>();
            List<AnuncioResponse> anuncioResponses = new List<AnuncioResponse>();
            var anuncios = await _anuncioRepository.ConsultarAnunciosAsync(idUsuario);

            if (anuncios is null)
            {
                response.Message = $"No se pudo obtener información de anuncios del usuario {idUsuario}";
                return response;
            }

            foreach (var anuncio in anuncios)
            {
                var anuncioDetalle = await _anuncioDetalleRepository.ConsultarAnuncioDetallePorAnuncioAsync(anuncio.IdAnuncio);
                var tipoPropiedad = await _tipoPropiedadRepository.ConsultarTipoPropiedadAsync(anuncio.IdTipoPropiedad);
                var usuario = await _usuarioRepository.ConsultarUsuarioAsync(anuncio.IdUsuario);
                var ubicacion = await _ubicacionRepository.ConsultarPorAnuncioAsync(anuncio.IdAnuncio);
                var evaluaciones = await _evaluacionRepository.ConsultarPorAnuncioAsync(anuncio.IdAnuncio);
                var imagenes = await _imagenRepository.ConsultarPorAnuncioAsync(anuncio.IdAnuncio);

                var anuncioResponse = _mapper.Map<AnuncioResponse>(anuncio);
                var usuarioResponse = usuario is null ? new UsuarioResponse() : _mapper.Map<UsuarioResponse>(usuario);
                var tipoPropiedadResponse = usuario is null ? new TipoPropiedadResponse() : _mapper.Map<TipoPropiedadResponse>(tipoPropiedad);
                var evaluacionResponses = _mapper.Map<IEnumerable<EvaluacionResponse>>(evaluaciones);
                var imagenResponses = _mapper.Map<IEnumerable<ImagenResponse>>(imagenes);
                ubicacion = ubicacion ?? new UbicacionEntity();

                anuncioResponse.Usuario = usuarioResponse;
                anuncioResponse.TipoPropiedad = tipoPropiedadResponse;
                anuncioResponse.Metros2 = anuncioDetalle.Metros2;
                anuncioResponse.CantidadHabitaciones = anuncioDetalle.CantidadHabitaciones;
                anuncioResponse.CantidadBaños = anuncioDetalle.CantidadBaños;
                anuncioResponse.CantidadParqueos = anuncioDetalle.CantidadParqueos;
                anuncioResponse.Plantas = anuncioDetalle.Plantas;
                anuncioResponse.Direccion = ubicacion.Direccion;
                anuncioResponse.Latitud = ubicacion.Latitud;
                anuncioResponse.Longitud = ubicacion.Longitud;
                anuncioResponse.Evaluaciones = evaluacionResponses;
                anuncioResponse.Imagenes = imagenResponses;
                anuncioResponses.Add(anuncioResponse);
            }
            response.Success = true;
            response.Message = "Se obtuvo información de anuncios exitosamente.";
            response.Data = anuncioResponses;
            return response;
        }

        public async Task<BaseServiceResponse<int>> CrearAsync(int idUsuario, CreacionAnuncioRequest request)
        {
            BaseServiceResponse<int> response = new BaseServiceResponse<int>();

            var anuncio = _mapper.Map<AnuncioEntity>(request);
            anuncio.IdUsuario = idUsuario;
            var idAnuncio = await _anuncioRepository.CrearAnuncioAsync(anuncio);
            if (idAnuncio == default)
            {
                response.Message = "No se puedo registrar el anuncio.";
                return response;
            }
            
            var anuncioDetalle = _mapper.Map<AnuncioDetalleEntity>(request);
            anuncioDetalle.IdAnuncio = idAnuncio;
            var idAnuncioDetalle = await _anuncioDetalleRepository.CrearAnuncioDetalleAsync(anuncioDetalle);
            if (idAnuncioDetalle == default)
            {
                response.Message = "No se puedo registrar el detalle del anuncio.";
                return response;
            }

            var ubicacion = _mapper.Map<UbicacionEntity>(request);
            ubicacion.IdAnuncio = idAnuncio;
            var idUbicacion = await _ubicacionRepository.CrearUbicacionAsync(ubicacion);
            if (idUbicacion == default)
            {
                response.Message = "No se puedo registrar la ubicación.";
                return response;
            }

            response.Data = idAnuncio;
            response.Success = true;
            response.Message = "Se registró exitosamente";

            return response;
        }

        public async Task<BaseServiceResponse<bool>> EditarAsync(int idUsuario, EdicionAnuncioRequest request)
        {
            BaseServiceResponse<bool> response = new BaseServiceResponse<bool>();
           
            var anuncio = _mapper.Map<AnuncioEntity>(request);
            anuncio.IdUsuario = idUsuario;
            var anuncioUpdated = await _anuncioRepository.EditarAnuncioAsync(anuncio);
            if (!anuncioUpdated)
            {
                response.Message = "No se puedo editar el anuncio.";
                return response;
            }

            var anuncioDetalle = _mapper.Map<AnuncioDetalleEntity>(request);
            var anuncioDetalleEntity = await _anuncioDetalleRepository.ConsultarAnuncioDetallePorAnuncioAsync(request.IdAnuncio);
            anuncioDetalle.IdAnuncioDetalle = anuncioDetalleEntity.IdAnuncioDetalle;
            var anuncioDetalleUpdated = await _anuncioDetalleRepository.EditarAnuncioDetalleAsync(anuncioDetalle);
            if (!anuncioDetalleUpdated)
            {
                response.Message = "No se puedo editar el detalle del anuncio.";
                return response;
            }

            var ubicacion = _mapper.Map<UbicacionEntity>(request);
            var ubicacionEntity = await _ubicacionRepository.ConsultarPorAnuncioAsync(request.IdAnuncio);
            ubicacion.IdUbicacion = ubicacionEntity.IdUbicacion;
            var ubicacionUpdated = await _ubicacionRepository.EditarUbicacionAsync(ubicacion);
            if (!ubicacionUpdated)
            {
                response.Message = "No se puedo editar la ubicación.";
                return response;
            }

            response.Data = anuncioUpdated;
            response.Success = anuncioUpdated;
            response.Message = "Se actualizó exitosamente";

            return response;
        }

        public async Task<BaseServiceResponse<bool>> EliminarAsync(int idUsuario, int id)
        {
            BaseServiceResponse<bool> response = new BaseServiceResponse<bool>();

            var deleted = await _anuncioRepository.EliminarAnuncioAsync(id);
            if (!deleted)
            {
                response.Message = "No se puedo editar el anuncio.";
                return response;
            }

            response.Data = deleted;
            response.Success = deleted;
            response.Message = "Se eliminó exitosamente";

            return response;
        }

        
    }
}
