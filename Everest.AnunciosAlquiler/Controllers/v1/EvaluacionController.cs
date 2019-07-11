using Everest.Common.Enums;
using Everest.Services.Interfaces;
using Everest.ViewModels;
using Everest.ViewModels.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Everest.AnunciosAlquiler.Controllers.v1
{
    [Route("api/v1/usuarios/{idUsuario}/anuncios/{idAnuncio}/evaluaciones")]
    [ApiController]
    public class EvaluacionController : ControllerBase
    {
        private readonly IEvaluacionService _evaluacionService;
        private readonly IUsuarioService _usuarioService;
        public EvaluacionController(IEvaluacionService evaluacionService, IUsuarioService usuarioService)
        {
            _evaluacionService = evaluacionService;
            _usuarioService = usuarioService;
        }

        #region Privates Methods
        private async Task<BaseServiceResponse<int>> ValidarPropietario(int idUsuario)
        {
            BaseServiceResponse<int> response = new BaseServiceResponse<int>();
            var usuario = await _usuarioService.ConsultarUsuarioAsync(idUsuario);
            response.Data = idUsuario;
            if (usuario.Data?.IdRol == (int)RolEnums.Propietario)
            {
                response.Message = "El usuario no puede registrar una evaluación porque tiene rol Propietario.";
                return response;
            }

            response.Success = true;
            return response;
        }
        #endregion

        [HttpPost]
        public async Task<IActionResult> Crear(int idUsuario, int idAnuncio, CreacionEvaluacionRequest request)
        {
            try
            {
                var responseUser = await ValidarPropietario(idUsuario);
                if (!responseUser.Success)
                    return StatusCode(StatusCodes.Status403Forbidden, responseUser.Message);

                var response = await _evaluacionService.CrearEvaluacionAsync(idAnuncio, request);
                if (!response.Success)
                    return StatusCode(StatusCodes.Status400BadRequest, responseUser.Message);

                return Created("", response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}