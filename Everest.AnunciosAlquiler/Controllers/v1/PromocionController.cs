using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Everest.Common.Enums;
using Everest.Services.Interfaces;
using Everest.ViewModels;
using Everest.ViewModels.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        #region Privates Methods
        private async Task<BaseServiceResponse<int>> ValidarAdministrador(int idUsuario)
        {
            BaseServiceResponse<int> response = new BaseServiceResponse<int>();
            var usuario = await _usuarioService.ConsultarUsuarioAsync(idUsuario);
            response.Data = idUsuario;
            if (usuario.Data?.IdRol != (int)RolEnums.Administrador)
            {
                response.Message = "Debe tener rol de admnistrador para poder registrar una foto.";
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
                response.Message = "Debe tener rol de inquilino para poder registrar una foto.";
                return response;
            }

            response.Success = true;
            return response;
        }
        #endregion

        [HttpGet]
        public async Task<IActionResult> ConsultarPromocionAsync(int idUsuario)
        {
            try
            {
                var responseUser = await ValidarInquilino(idUsuario);
                if (!responseUser.Success)
                    return Forbid(responseUser.Message);

                var response = await _promocionService.ConsultarPromocionAsync();
                if (!response.Success)
                    return NotFound(response.Message);

                return Ok(response);
            }
            catch (Exception ex)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Content(ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> GenerarPromocionAnuncioAsync(int idUsuario)
        {
            try
            {
                var responseUser = await ValidarInquilino(idUsuario);
                if (!responseUser.Success)
                    return Forbid(responseUser.Message);

                var response = await _promocionService.GenerarPromocionAnuncioAsync();
                if (!response.Success)
                    return BadRequest(response.Message);

                return Created("", response);
            }
            catch (Exception ex)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Content(ex.Message);
            }

        }

        [HttpPut()]
        public async Task<IActionResult> AgendarPromocionAnuncioAsync(int idUsuario, [FromBody]AgendarPromocionAnuncioRequest request)
        {
            try
            {
                var responseUser = await ValidarAdministrador(idUsuario);
                if (!responseUser.Success)
                    return Forbid(responseUser.Message);

                var response = await _promocionService.AgendarPromocionAnuncioAsync(request);
                if (!response.Success)
                    return BadRequest(response.Message);

                return Ok(response);
            }
            catch (Exception ex)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Content(ex.Message);
            }

        }
    }
}