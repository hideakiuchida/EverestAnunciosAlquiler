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
    [Route("api/anuncios")]
    [ApiController]
    public class AnuncioController : ControllerBase
    {
        private readonly IAnuncioService _anuncioService;
        private readonly IUsuarioService _usuarioService;
        public AnuncioController(IAnuncioService anuncioService, IUsuarioService usuarioService)
        {
            _anuncioService = anuncioService;
            _usuarioService = usuarioService;
        }

        #region Privates Methods
        private async Task<BaseServiceResponse<int>> ValidarPropietario(int idUsuario)
        {
            BaseServiceResponse<int> response = new BaseServiceResponse<int>();
            var usuario = await _usuarioService.ConsultarUsuarioAsync(idUsuario);
            response.Data = idUsuario;
            if (usuario.Data?.IdRol != (int)RolEnums.Propietario)
                response.Message = "Debe tener rol de propietario para poder crear un anuncio.";

            response.Success = true;
            return response;
        }
        #endregion


        [HttpPost]
        public async Task<IActionResult> Crear(BaseServiceRequest<CreacionAnuncioRequest> request)
        {
            try
            {
                var responseUser = await ValidarPropietario(request.IdUsuario);
                if (!responseUser.Success)
                {
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    return Content(responseUser.Message);
                }

                var response = await _anuncioService.CrearAsync(request.Data);
                if (!response.Success)
                {
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.NoContent;
                    return Content(response.Message);
                }

                return Created("", response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Editar(BaseServiceRequest<EdicionAnuncioRequest> request)
        {
            try
            {
                var responseUser = await ValidarPropietario(request.IdUsuario);
                if (!responseUser.Success)
                {
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    return Content(responseUser.Message);
                }

                var response = await _anuncioService.EditarAsync(request.Data);
                if (!response.Success)
                {
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.NoContent;
                    return Content(response.Message);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Eliminar(BaseServiceRequest<int> request)
        {
            try
            {
                var responseUser = await ValidarPropietario(request.IdUsuario);
                if (!responseUser.Success)
                {
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    return Content(responseUser.Message);
                }

                var response = await _anuncioService.EliminarAsync(request.Data);
                if (!response.Success)
                {
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.NoContent;
                    return Content(response.Message);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}