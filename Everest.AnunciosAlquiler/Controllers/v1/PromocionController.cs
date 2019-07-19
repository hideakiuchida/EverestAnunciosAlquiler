using Everest.Common.Enums;
using Everest.Services.Interfaces;
using Everest.ViewModels;
using Everest.ViewModels.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Everest.AnunciosAlquiler.Controllers.v1
{
    [Route("api/v1/usuarios/{idUsuario}/promociones")]
    [ApiController]
    public class PromocionController : ControllerBase
    {
        private readonly IPromocionService _promocionService;
        private readonly IUsuarioService _usuarioService;
        public PromocionController(IPromocionService promocionService, IUsuarioService usuarioService)
        {
            _promocionService = promocionService;
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> ConsultarPromocionAsync(int idUsuario)
        {
            try
            {
                var responseUser = await ValidarInquilino(idUsuario);
                if (!responseUser.Success)
                    return StatusCode(StatusCodes.Status403Forbidden, responseUser.Message);

                var response = await _promocionService.ConsultarPromocionAsync(idUsuario);
                if (!response.Success)
                    return StatusCode(StatusCodes.Status404NotFound, response.Message);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost]
        [Route("generar")]
        public async Task<IActionResult> GenerarPromocionAnuncioAsync(int idUsuario)
        {
            try
            {
                var responseUser = await ValidarAdministrador(idUsuario);
                if (!responseUser.Success)
                    return StatusCode(StatusCodes.Status403Forbidden, responseUser.Message);

                var response = await _promocionService.GenerarPromocionAnuncioAsync();
                if (!response.Success)
                    return StatusCode(StatusCodes.Status400BadRequest, response.Message);

                return Created("", response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpPut()]
        [Route("agendar")]
        public async Task<IActionResult> AgendarPromocionAnuncioAsync(int idUsuario, [FromBody]AgendarPromocionAnuncioRequest request)
        {
            try
            {
                var responseUser = await ValidarInquilino(idUsuario);
                if (!responseUser.Success)
                    return StatusCode(StatusCodes.Status403Forbidden, responseUser.Message);

                var response = await _promocionService.AgendarPromocionAnuncioAsync(idUsuario, request);
                if (!response.Success)
                    return StatusCode(StatusCodes.Status400BadRequest, response.Message);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        #region Privates Methods
        private async Task<BaseServiceResponse<int>> ValidarAdministrador(int idUsuario)
        {
            BaseServiceResponse<int> response = new BaseServiceResponse<int>();
            var usuario = await _usuarioService.ConsultarUsuarioAsync(idUsuario);
            response.Data = idUsuario;
            if (usuario.Data?.IdRol != (int)RolEnums.Administrador)
            {
                response.Message = "Debe tener rol de admnistrador para poder generar una promoción.";
                return response;
            }

            response.Success = true;
            return response;
        }

        private async Task<BaseServiceResponse<int>> ValidarInquilino(int idUsuario)
        {
            BaseServiceResponse<int> response = new BaseServiceResponse<int>();
            var usuario = await _usuarioService.ConsultarUsuarioAsync(idUsuario);
            response.Data = idUsuario;
            if (usuario.Data?.IdRol != (int)RolEnums.Inquilino)
            {
                response.Message = "Debe tener rol de inquilino para poder ejecutar la solicitud.";
                return response;
            }

            response.Success = true;
            return response;
        }
        #endregion
    }
}