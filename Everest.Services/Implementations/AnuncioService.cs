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

        public async Task<BaseServiceResponse<bool>> ActivarAnuncioAsync(int id, bool esActivo)
        {
            BaseServiceResponse<bool> response = new BaseServiceResponse<bool>();
            var anuncioResult = await _anuncioRepository.ConsultarAsync(id);
            if (anuncioResult == null)
            {
                response.Message = "No existe el anuncio.";
                return response;
            }
            var success = await _anuncioRepository.ActivarAnuncioAsync(id, esActivo);
            response.Success = success;
            response.Message = "Se pudo actualizar exitosamente.";
            response.Data = esActivo;
            return response;
        }

        public async Task<BaseServiceResponse<IEnumerable<AnuncioResponse>>> ConsultarPorUsuarioAsync(string idUsuario)
        {
            BaseServiceResponse<IEnumerable<AnuncioResponse>> response = new BaseServiceResponse<IEnumerable<AnuncioResponse>>();
            List<AnuncioResponse> anuncioResponses = new List<AnuncioResponse>();
            var anuncios = await _anuncioRepository.ConsultarAnunciosAsync(idUsuario);
            var usuario = await _usuarioRepository.ConsultarUsuarioAsync(idUsuario);
            if (anuncios is null)
            {
                response.Message = $"No se pudo obtener información de anuncios del usuario {idUsuario}";
                return response;
            }

            foreach (var anuncio in anuncios)
            {
                var anuncioDetalle = await _anuncioDetalleRepository.ConsultarAnuncioDetallePorAnuncioAsync(anuncio.IdAnuncio);
                var tipoPropiedad = await _tipoPropiedadRepository.ConsultarTipoPropiedadAsync(anuncio.IdTipoPropiedad);
                var ubicacion = await _ubicacionRepository.ConsultarPorAnuncioAsync(anuncio.IdAnuncio);
                var evaluaciones = await _evaluacionRepository.ConsultarPorAnuncioAsync(anuncio.IdAnuncio);
                var imagenes = await _imagenRepository.ConsultarPorAnuncioAsync(anuncio.IdAnuncio);

                var anuncioResponse = _mapper.Map<AnuncioResponse>(anuncio);
                var usuarioResponse = usuario is null ? new UsuarioResponse() : _mapper.Map<UsuarioResponse>(usuario);
                var tipoPropiedadResponse = tipoPropiedad is null ? new TipoPropiedadResponse() : _mapper.Map<TipoPropiedadResponse>(tipoPropiedad);
                var evaluacionResponses = _mapper.Map<IEnumerable<EvaluacionResponse>>(evaluaciones);
                var imagenResponses = _mapper.Map<IEnumerable<ImagenResponse>>(imagenes);
                ubicacion = ubicacion ?? new UbicacionEntity();
                anuncioDetalle = anuncioDetalle ?? new AnuncioDetalleEntity();

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

        public async Task<BaseServiceResponse<int>> CrearAsync(string idUsuario, CreacionAnuncioRequest request)
        {
            BaseServiceResponse<int> response = new BaseServiceResponse<int>();
            var usuario = await _usuarioRepository.ConsultarUsuarioAsync(idUsuario);
            var anuncio = _mapper.Map<AnuncioEntity>(request);
            anuncio.IdUsuario = usuario.IdUsuario;
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

        public async Task<BaseServiceResponse<bool>> EditarAsync(string idUsuario, EdicionAnuncioRequest request)
        {
            BaseServiceResponse<bool> response = new BaseServiceResponse<bool>();
            var usuario = await _usuarioRepository.ConsultarUsuarioAsync(idUsuario);
            var anuncioResult = await _anuncioRepository.ConsultarAsync(request.IdAnuncio.Value);
            if (anuncioResult == null)
            {
                response.Message = "No existe el anuncio.";
                return response;
            }
            if (anuncioResult.Activo)
            {
                response.Message = "No se puedo editar el anuncio porque se encuentra activo.";
                return response;
            }

            var anuncio = _mapper.Map<AnuncioEntity>(request);
            anuncio.IdUsuario = usuario.IdUsuario;
            var anuncioUpdated = await _anuncioRepository.EditarAnuncioAsync(anuncio);
            if (!anuncioUpdated)
            {
                response.Message = "No se puedo editar el anuncio.";
                return response;
            }

            var anuncioDetalle = _mapper.Map<AnuncioDetalleEntity>(request);
            var anuncioDetalleEntity = await _anuncioDetalleRepository.ConsultarAnuncioDetallePorAnuncioAsync(request.IdAnuncio.Value);
            anuncioDetalle.IdAnuncioDetalle = anuncioDetalleEntity != default ? anuncioDetalleEntity.IdAnuncioDetalle : default;
            var anuncioDetalleUpdated = await _anuncioDetalleRepository.EditarAnuncioDetalleAsync(anuncioDetalle);
            if (!anuncioDetalleUpdated)
            {
                response.Message = "No se puedo editar el detalle del anuncio.";
                return response;
            }

            var ubicacion = _mapper.Map<UbicacionEntity>(request);
            var ubicacionEntity = await _ubicacionRepository.ConsultarPorAnuncioAsync(request.IdAnuncio.Value);
            ubicacion.IdUbicacion = ubicacionEntity != default ? ubicacionEntity.IdUbicacion : default;
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

        public async Task<BaseServiceResponse<bool>> EliminarAsync(int id)
        {
            BaseServiceResponse<bool> response = new BaseServiceResponse<bool>();

            var anuncioResult = await _anuncioRepository.ConsultarAsync(id);
            if (anuncioResult == null)
            {
                response.Message = "No existe el anuncio.";
                return response;
            }
            if (anuncioResult.Activo)
            {
                response.Message = "No se puedo eliminar el anuncio porque se encuentra activo.";
                return response;
            }

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
