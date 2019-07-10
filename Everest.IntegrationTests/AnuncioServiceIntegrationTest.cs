﻿using AutoMapper;
using Everest.AnunciosAlquiler.Helpers;
using Everest.Repository.Implementations;
using Everest.Repository.Interfaces;
using Everest.Services.Implementations;
using Everest.Services.Interfaces;
using Everest.ViewModels.Fakes;
using Everest.ViewModels.Request;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Everest.IntegrationTests
{
    class AnuncioServiceIntegrationTest
    {
        private IAnuncioService _anuncioService;

        #region Private Methods and Parameters for Mock
        private int ValidOwnerUserId = 1;
        private int ValidAnuncioId = 1;
        private CreacionAnuncioRequest ValidCreacionAnuncioRequest;
        private EdicionAnuncioRequest ValidEdicionAnuncioRequest;
        private int InvalidAnuncioId = default;

        public AnuncioServiceIntegrationTest()
        {
            //Arrange
            IDbConnection dbConnection = new SqlConnection("Server=HIDEAKIUCHIDA;Database=EVERESTDB;Integrated Security=True;");
            IAnuncioRepository anuncioRepository = new AnuncioRepository(dbConnection);
            IAnuncioDetalleRepository anuncioDetalleRepository = new AnuncioDetalleRepository(dbConnection);
            IUbicacionRepository ubicacionRepository = new UbicacionRepository(dbConnection);
            IUsuarioRepository usuarioRepository = new UsuarioRepository(dbConnection);
            ITipoPropiedadRepository tipoPropiedadRepository = new TipoPropiedadRepository(dbConnection);
            IEvaluacionRepository evaluacionRepository = new EvaluacionRepository(dbConnection);
            IImagenRepository imagenRepository = new ImagenRepository(dbConnection);
            IMapper mapper = new Mapper(
                new MapperConfiguration(
                    configure => { configure.AddProfile<AutoMapperProfiles>(); }
                )
            );
            _anuncioService = new AnuncioService(anuncioRepository, anuncioDetalleRepository, usuarioRepository, 
                tipoPropiedadRepository, ubicacionRepository, evaluacionRepository, imagenRepository, mapper);

            ValidCreacionAnuncioRequest = AnuncioFake.GetCreacionAnuncioRequest();
            ValidEdicionAnuncioRequest = AnuncioFake.GetEdicionAnuncioRequest();
        }
        #endregion

        #region Success Paths
        [Test]
        public async Task Given_ValidUserId_When_ConsultarPorUsuarioAsync_Then_ReturnsBaseServiceResponseWithSuccess()
        {
            //Act
            var response = await _anuncioService.ConsultarPorUsuarioAsync(ValidOwnerUserId);

            //Assert
            Assert.IsTrue(response.Success);
            Assert.IsTrue(response.Data.ToList().Count > 0);
        }

        [Test]
        public async Task Given_ValidUserIdAndValidCreacionAnuncioRequest_When_CrearAsync_Then_ReturnsBaseServiceResponseWithSuccess()
        {
            //Act
            var response = await _anuncioService.CrearAsync(ValidOwnerUserId, ValidCreacionAnuncioRequest);

            //Assert
            Assert.IsTrue(response.Success);
            Assert.IsTrue(response.Data != default);
        }

        [Test]
        public async Task Given_ValidUserIdAndValidEdicionAnuncioRequest_When_EditarAsync_Then_ReturnsBaseServiceResponseWithSuccess()
        {
            //Act
            var response = await _anuncioService.EditarAsync(ValidOwnerUserId, ValidEdicionAnuncioRequest);

            //Assert
            Assert.IsTrue(response.Success);
            Assert.IsTrue(response.Data);
        }

        [Test]
        public async Task Given_ValidUserIdAndValidAnuncioId_When_EliminarAsync_Then_ReturnsBaseServiceResponseWithSuccess()
        {
            //Act
            var response = await _anuncioService.EliminarAsync(ValidOwnerUserId, ValidAnuncioId);

            //Assert
            Assert.IsTrue(response.Success);
            Assert.IsTrue(response.Data);
        }
        #endregion

        #region Unsuccessful Paths
        [Test]
        public async Task Given_ValidUserIdAndInvalidCreacionAnuncioRequest_When_CrearAsync_Then_ReturnsBaseServiceResponseWithSuccess()
        {
            //Act
            var response = await _anuncioService.CrearAsync(ValidOwnerUserId, default);

            //Assert
            Assert.IsNotNull(response.Data);
            Assert.IsTrue(response.Success);
        }

        [Test]
        public async Task Given_ValidUserIdAndInvalidEdicionAnuncioRequest_When_EditarAsync_Then_ReturnsBaseServiceResponseWithSuccess()
        {
            //Act
            var response = await _anuncioService.EditarAsync(ValidOwnerUserId, default);

            //Assert
            Assert.IsNotNull(response.Data);
            Assert.IsTrue(response.Success);
        }


        [Test]
        public async Task Given_ValidUserIdAndInvalidAnuncioId_When_EliminarAsync_Then_ReturnsBaseServiceResponseWithSuccess()
        {
            //Act
            var response = await _anuncioService.EliminarAsync(ValidOwnerUserId, InvalidAnuncioId);

            //Assert
            Assert.IsNotNull(response.Data);
            Assert.IsTrue(response.Success);
        }
        #endregion
    }
}