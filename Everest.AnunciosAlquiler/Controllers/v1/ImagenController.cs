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
    [Route("api/v1/usuarios/{idUsuario}/anuncios/{idAnuncio}/imagenes")]
    [ApiController]
    public class ImagenController : ControllerBase
    {
        private readonly IImagenService _imagenService;
        private readonly IUsuarioService _usuarioService;
        public ImagenController(IImagenService imagenService, IUsuarioService usuarioService)
        {
            _imagenService = imagenService;
            _usuarioService = usuarioService;
        }

        #region Privates Methods
        private async Task<BaseServiceResponse<int>> ValidarPropietario(int idUsuario)
        {
            BaseServiceResponse<int> response = new BaseServiceResponse<int>();
            var usuario = await _usuarioService.ConsultarUsuarioAsync(idUsuario);
            response.Data = idUsuario;
            if (usuario.Data?.IdRol != (int)RolEnums.Propietario)
            {
                response.Message = "Debe tener rol de propietario para poder registrar una foto.";
                return response;
            }

            response.Success = true;
            return response;
        }
        #endregion


        [HttpPost]
        public async Task<IActionResult> Crear(int idUsuario, int idAnuncio, [FromForm]CreacionImagenRequest request)
        {
            try
            {
                var responseUser = await ValidarPropietario(idUsuario);
                if (!responseUser.Success)
                    return StatusCode(StatusCodes.Status403Forbidden, responseUser.Message);

                var response = await _imagenService.CrearImagenAsync(idAnuncio, request);
                if (!response.Success)
                    return StatusCode(StatusCodes.Status400BadRequest, responseUser.Message);

                return Created("", response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int idUsuario, int idAnuncio, int id)
        {
            try
            {
                var responseUser = await ValidarPropietario(idUsuario);
                if (!responseUser.Success)
                    return StatusCode(StatusCodes.Status403Forbidden, responseUser.Message);

                var response = await _imagenService.EliminarAsync(idAnuncio, id);
                if (!response.Success)
                    return StatusCode(StatusCodes.Status400BadRequest, responseUser.Message);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }

    }
}