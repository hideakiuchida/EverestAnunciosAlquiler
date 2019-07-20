using AutoMapper;
using Everest.Common.Utils;
using Everest.Entities;
using Everest.Repository.Interfaces;
using Everest.Services.Interfaces;
using Everest.ViewModels;
using Everest.ViewModels.Request;
using Everest.ViewModels.Response;
using System.Threading.Tasks;

namespace Everest.Services.Implementations
{
    public class PromocionService : IPromocionService
    {
        private readonly IAnuncioRepository _anuncioRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPromocionAnuncioRepository _promocionAnuncioRepository;
        private readonly IMapper _mapper;

        public PromocionService(IAnuncioRepository anuncioRepository, IPromocionAnuncioRepository promocionAnuncioRepository, IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _anuncioRepository = anuncioRepository;
            _usuarioRepository = usuarioRepository;
            _promocionAnuncioRepository = promocionAnuncioRepository;
            _mapper = mapper;
        }
        public async Task<BaseServiceResponse<bool>> AgendarPromocionAnuncioAsync(string idUsuario, AgendarPromocionAnuncioRequest request)
        {
            BaseServiceResponse<bool> response = new BaseServiceResponse<bool>();

            var agendado = ThreadPromotion.AgendarPromocionParaUsuario(idUsuario, request.IdAnuncio);
            if (!agendado)
            {
                response.Message = $"No se pudo agendar la promoción para el usuario {idUsuario} con el anuncio {request.IdAnuncio}.";
                return response;
            }
            var usuario = await _usuarioRepository.ConsultarUsuarioAsync(idUsuario);
            var promocionEntity = _mapper.Map<PromocionAnuncioEntity>(request);
            promocionEntity.IdUsuario = usuario.IdUsuario;
            var result = await _promocionAnuncioRepository.AgendarPromocionAnuncioAsync(promocionEntity);
            if (result == default)
            {
                response.Message = "No se pudo registrar la agenda de la promoción.";
                return response;
            }

            response.Message = "Se pudo agendar exitosamente.";
            response.Success = true;
            response.Data = result;
            return response;

        }

        public async Task<BaseServiceResponse<PromocionAnuncioResponse>> ConsultarPromocionAsync(string idUsuario)
        {
            BaseServiceResponse<PromocionAnuncioResponse> response = new BaseServiceResponse<PromocionAnuncioResponse>();
            var result = await _promocionAnuncioRepository.ConsultarPromocionAsync();
            if (result is null)
            {
                response.Message = "No se pudo obtener información.";
                return response;
            }

            ThreadPromotion.ActivarPromocionParaUsuario(idUsuario, result.IdAnuncio);

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

            var idPromocionAnuncio = await _promocionAnuncioRepository.CrearPromocionAnuncioAsync(anuncioEntity.IdAnuncio);
            if (idPromocionAnuncio == default)
            {
                response.Message = "No se pudo obtener generar promoción.";
                return response;
            }

            ThreadPromotion.GenerarPromocion(anuncioEntity.IdAnuncio);

            response.Message = "Se obtuvo la información exitosamente.";
            response.Success = true;
            response.Data = idPromocionAnuncio;
            return response;
        }
    }
}
