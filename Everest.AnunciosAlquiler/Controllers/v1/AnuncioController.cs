﻿using Everest.Common.Enums;
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
        private async Task<BaseServiceResponse<int>> ValidarPropietario(int idUsuario)
        {
            BaseServiceResponse<int> response = new BaseServiceResponse<int>();
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
        public async Task<IActionResult> ConsultarAnunciosPorIdUsuario(int idUsuario)
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
        public async Task<IActionResult> Crear(int idUsuario, CreacionAnuncioRequest request)
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
        public async Task<IActionResult> Editar(int idUsuario, EdicionAnuncioRequest request)
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
        public async Task<IActionResult> Activar(int idUsuario, ActivarAnuncioRequest request)
        {
            try
            {
                var responseUser = await ValidarPropietario(idUsuario);
                if (!responseUser.Success)
                    return StatusCode(StatusCodes.Status403Forbidden, responseUser.Message);

                var response = await _anuncioService.ActivarAnuncioAsync(request.IdAnuncio, request.EsActivo);
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
        public async Task<IActionResult> Eliminar(int idUsuario, int id)
        {
            try
            {
                var responseUser = await ValidarPropietario(idUsuario);
                if (!responseUser.Success)
                    return StatusCode(StatusCodes.Status403Forbidden, responseUser.Message);

                var response = await _anuncioService.EliminarAsync(idUsuario, id);
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