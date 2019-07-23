using Everest.Common.Enums;
using Everest.Services.Interfaces;
using Everest.ViewModels;
using Everest.ViewModels.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Everest.AnunciosAlquiler.Controllers.v1
{
    [Route("api/v1/usuarios/{idUsuario}/anuncios")]
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
        private async Task<BaseServiceResponse<string>> ValidarPropietario(string idUsuario)
        {
            BaseServiceResponse<string> response = new BaseServiceResponse<string>();
            var usuario = await _usuarioService.ConsultarUsuarioAsync(idUsuario);
            response.Data = idUsuario;
            if (usuario.Data?.IdRol != (int)RolEnums.Propietario)
            {
                response.Message = "Debe tener rol de propietario para poder crear un anuncio.";
                return response;
            }

            response.Success = true;
            return response;
        }
        #endregion

        [HttpGet]
        public async Task<IActionResult> ConsultarAnunciosPorIdUsuario(string idUsuario)
        {
            try
            {
                var responseUser = await ValidarPropietario(idUsuario);
                if (!responseUser.Success)
                    return StatusCode(StatusCodes.Status403Forbidden, responseUser.Message);

                var response = await _anuncioService.ConsultarPorUsuarioAsync(idUsuario);
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
        public async Task<IActionResult> Crear(string idUsuario, CreacionAnuncioRequest request)
        {
            try
            {
                var responseUser = await ValidarPropietario(idUsuario);
                if (!responseUser.Success)
                    return StatusCode(StatusCodes.Status403Forbidden, responseUser.Message);


                var response = await _anuncioService.CrearAsync(idUsuario, request);
                if (!response.Success)
                    return StatusCode(StatusCodes.Status400BadRequest, response.Message);

                return Created("", response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Editar(string idUsuario, EdicionAnuncioRequest request)
        {
            try
            {
                var responseUser = await ValidarPropietario(idUsuario);
                if (!responseUser.Success)
                    return StatusCode(StatusCodes.Status403Forbidden, responseUser.Message);

                var response = await _anuncioService.EditarAsync(idUsuario, request);
                if (!response.Success)
                    return StatusCode(StatusCodes.Status400BadRequest, response.Message);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPatch]
        [Route("activacion")]
        public async Task<IActionResult> Activar(string idUsuario, ActivarAnuncioRequest request)
        {
            try
            {
                var responseUser = await ValidarPropietario(idUsuario);
                if (!responseUser.Success)
                    return StatusCode(StatusCodes.Status403Forbidden, responseUser.Message);

                var response = await _anuncioService.ActivarAnuncioAsync(request.IdAnuncio.Value, request.EsActivo.Value);
                if (!response.Success)
                    return StatusCode(StatusCodes.Status400BadRequest, response.Message);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(string idUsuario, int id)
        {
            try
            {
                var responseUser = await ValidarPropietario(idUsuario);
                if (!responseUser.Success)
                    return StatusCode(StatusCodes.Status403Forbidden, responseUser.Message);

                var response = await _anuncioService.EliminarAsync(id);
                if (!response.Success)
                    return StatusCode(StatusCodes.Status400BadRequest, response.Message);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}