using AutoMapper;
using Everest.Entities;
using Everest.Repository.Interfaces;
using Everest.Services.Interfaces;
using Everest.ViewModels;
using Everest.ViewModels.Request;
using System.Threading.Tasks;

namespace Everest.Services.Implementations
{
    public class EvaluacionService : IEvaluacionService
    {
        private readonly IEvaluacionRepository _evaluacionRepository;
        private readonly IMapper _mapper;
        public EvaluacionService(IEvaluacionRepository evaluacionRepository, IMapper mapper)
        {
            _evaluacionRepository = evaluacionRepository;
            _mapper = mapper;
        }
        public async Task<BaseServiceResponse<int>> CrearEvaluacionAsync(int idAnuncio, CreacionEvaluacionRequest request)
        {
            BaseServiceResponse<int> response = new BaseServiceResponse<int>();
            var evaluacionEntity = _mapper.Map<EvaluacionEntity>(request);
            evaluacionEntity.IdAnuncio = idAnuncio;
            var idEvaluacion = await _evaluacionRepository.CrearEvaluacionAsync(evaluacionEntity);

            if (idEvaluacion == default)
            {
                response.Message = "No se puedo registrar la evaluación.";
                return response;
            }

            response.Message = "Se registró la evaluación exitosamente.";
            response.Success = true;
            response.Data = idEvaluacion;

            return response;
        }
    }
}
