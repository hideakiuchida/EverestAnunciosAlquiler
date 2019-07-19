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
        private async Task<BaseServiceResponse<string>> ValidarPropietario(string idUsuario, int idAnuncio)
        {
            BaseServiceResponse<string> response = new BaseServiceResponse<string>();
            var usuario = await _usuarioService.ConsultarUsuarioPorAnuncioAsync(idAnuncio);
            
            if (usuario.Data?.Identifier == idUsuario)
            {
                response.Message = "El usuario no puede registrar una evaluación.";
                return response;
            }
            response.Data = idUsuario;
            response.Success = true;
            return response;
        }
        #endregion

        [HttpPost]
        public async Task<IActionResult> Crear(string idUsuario, int idAnuncio, CreacionEvaluacionRequest request)
        {
            try
            {
                var responseUser = await ValidarPropietario(idUsuario, idAnuncio);
                if (!responseUser.Success)
                    return StatusCode(StatusCodes.Status403Forbidden, responseUser.Message);

                var response = await _evaluacionService.CrearEvaluacionAsync(idAnuncio, request);
                if (!response.Success)
                    return StatusCode(StatusCodes.Status400BadRequest, response.Message);

                return Created("", response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}