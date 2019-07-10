using AutoMapper;
using Everest.Entities;
using Everest.Repository.Interfaces;
using Everest.Services.Interfaces;
using Everest.ViewModels;
using Everest.ViewModels.Request;
using Everest.ViewModels.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Everest.Services.Implementations
{
    public class PromocionService : IPromocionService
    {
        private readonly IAnuncioRepository _anuncioRepository;
        private readonly IPromocionAnuncioRepository _promocionAnuncioRepository;
        private readonly IMapper _mapper;
        public PromocionService(IAnuncioRepository anuncioRepository, IPromocionAnuncioRepository promocionAnuncioRepository, IMapper mapper)
        {
            _anuncioRepository = anuncioRepository;
            _promocionAnuncioRepository = promocionAnuncioRepository;
            _mapper = mapper;
        }
        public async Task<BaseServiceResponse<bool>> AgendarPromocionAnuncioAsync(AgendarPromocionAnuncioRequest request)
        {
            BaseServiceResponse<bool> response = new BaseServiceResponse<bool>();
            var promocionEntity = _mapper.Map<PromocionAnuncioEntity>(request);
            var result = await _promocionAnuncioRepository.AgendarPromocionAnuncioAsync(promocionEntity);
            if (result == default)
            {
                response.Message = "No se pudo obtener información.";
                return response;
            }
            response.Message = "Se obtuvo la información exitosamente.";
            response.Success = true;
            response.Data = result;
            return response;

        }

        public async Task<BaseServiceResponse<PromocionAnuncioResponse>> ConsultarPromocionAsync()
        {
            //Thread 3 minutes
            BaseServiceResponse<PromocionAnuncioResponse> response = new BaseServiceResponse<PromocionAnuncioResponse>();
            var result = await _promocionAnuncioRepository.ConsultarPromocionAsync();
            if (result is null)
            {
                response.Message = "No se pudo obtener información.";
                return response;
            }

            var promocion = _mapper.Map<PromocionAnuncioResponse>(result);
            response.Message = "Se obtuvo la información exitosamente.";
            response.Success = true;
            response.Data = promocion;
            return response;
        }

        public async Task<BaseServiceResponse<int>> GenerarPromocionAnuncioAsync()
        {
            BaseServiceResponse<int> response = new BaseServiceResponse<int>();
            var anuncioEntity = await _anuncioRepository.ConsultarAnuncioMasAntiguoAsync();
            if (anuncioEntity is null)
            {
                response.Message = "No se pudo obtener información.";
                return response;
            }

            var idPromocion = await _promocionAnuncioRepository.CrearPromocionAnuncioAsync(anuncioEntity.IdAnuncio);
            if (idPromocion == default)
            {
                response.Message = "No se pudo obtener generar promoción.";
                return response;
            }

            response.Message = "Se obtuvo la información exitosamente.";
            response.Success = true;
            response.Data = idPromocion;
            return response;
        }
    }
}
