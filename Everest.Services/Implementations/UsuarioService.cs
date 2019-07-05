using System.Threading.Tasks;
using AutoMapper;
using Everest.Repository.Interfaces;
using Everest.Services.Interfaces;
using Everest.ViewModels;
using Everest.ViewModels.Response;

namespace Everest.Services.Implementations
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }
        public async Task<BaseServiceResponse<UsuarioResponse>> ConsultarUsuarioAsync(int idUsuario)
        {
            BaseServiceResponse<UsuarioResponse> response = new BaseServiceResponse<UsuarioResponse>();
            var usuario = await _usuarioRepository.ConsultarUsuarioAsync(idUsuario);
            if (usuario is null)
            {
                response.Message = $"No se puedo obtener el usuario con idUsuario: {idUsuario}.";
                return response;
            }
            var usuarioResponse = _mapper.Map<UsuarioResponse>(usuario);
            response.Data = usuarioResponse;
            response.Success = true;
            response.Message = "Se obtuvo la información del usuario exitosamente";
            return response;
        }
    }
}
