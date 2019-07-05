using AutoMapper;
using Everest.Common.Enums;
using Everest.Entities;
using Everest.Repository.Interfaces;
using Everest.Services.Interfaces;
using Everest.ViewModels;
using Everest.ViewModels.Request;
using Everest.ViewModels.Response;
using System;
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
        public AnuncioService(IAnuncioRepository anuncioRepository, IAnuncioDetalleRepository anuncioDetalleRepository, IUsuarioRepository usuarioRepository, 
            IUbicacionRepository ubicacionRepository, IMapper mapper)
        {
            _anuncioRepository = anuncioRepository;
            _anuncioDetalleRepository = anuncioDetalleRepository;
            _usuarioRepository = usuarioRepository;
            _ubicacionRepository = ubicacionRepository;
            _mapper = mapper;
        }

        public async Task<BaseServiceResponse<IEnumerable<AnuncioResponse>>> ConsultarPorUsuarioAsync(int idUsuario)
        {
            var anuncios = await _anuncioRepository.ConsultarAnunciosAsync(idUsuario);
            foreach (var anuncio in anuncios)
            {

            }
        }

        public async Task<BaseServiceResponse<int>> CrearAsync(CreacionAnuncioRequest request)
        {
            BaseServiceResponse<int> response = new BaseServiceResponse<int>();

            var anuncio = _mapper.Map<AnuncioEntity>(request);
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

        public async Task<BaseServiceResponse<bool>> EditarAsync(EdicionAnuncioRequest request)
        {
            BaseServiceResponse<bool> response = new BaseServiceResponse<bool>();
           
            var anuncio = _mapper.Map<AnuncioEntity>(request);
            var anuncioUpdated = await _anuncioRepository.EditarAnuncioAsync(anuncio);
            if (!anuncioUpdated)
            {
                response.Message = "No se puedo editar el anuncio.";
                return response;
            }

            var anuncioDetalle = _mapper.Map<AnuncioDetalleEntity>(request);
            var anuncioDetalleUpdated = await _anuncioDetalleRepository.EditarAnuncioDetalleAsync(anuncioDetalle);
            if (!anuncioDetalleUpdated)
            {
                response.Message = "No se puedo editar el detalle del anuncio.";
                return response;
            }

            var ubicacion = _mapper.Map<UbicacionEntity>(request);
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
