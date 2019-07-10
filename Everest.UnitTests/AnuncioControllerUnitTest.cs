using Everest.AnunciosAlquiler.Controllers.v1;
using Everest.Common.Enums;
using Everest.Services.Interfaces;
using Everest.ViewModels;
using Everest.ViewModels.Fakes;
using Everest.ViewModels.Request;
using Everest.ViewModels.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Everest.UnitTests
{
    //Behaviour Driven Development 
    [TestFixture]
    public class AnuncioControllerUnitTest
    {
        private Mock<IAnuncioService> _anuncioServiceMock;
        private Mock<IUsuarioService> _usuarioServiceMock;


        #region Private Methods and Parameters for Mock
        private int ValidOwnerUserId = 1;
        private int ValidAnuncioId = 1;
        private CreacionAnuncioRequest ValidCreacionAnuncioRequest;
        private EdicionAnuncioRequest ValidEdicionAnuncioRequest;
        private int InvalidOwnerUserId = 3;
        private int InvalidAnuncioId = default;
       
        private async Task<BaseServiceResponse<UsuarioResponse>> GetUsuarioResponseAsync(bool isPropietario)
        {
            BaseServiceResponse<UsuarioResponse> response = new BaseServiceResponse<UsuarioResponse>();
            if (!isPropietario)
            {
                response.Data = UsuarioFake.GetUsuario(RolEnums.Inquilino);
                response.Message = "Se obtuvo existomante";
                response.Success = true;
                return await Task.FromResult(response);
            }
            response.Data = UsuarioFake.GetUsuario(RolEnums.Propietario);
            response.Message = "Se obtuvo existomante";
            response.Success = true;
            return await Task.FromResult(response);
        }

        private async Task<BaseServiceResponse<IEnumerable<AnuncioResponse>>> GetAnuncioResponseAsync(bool isValid)
        {
            BaseServiceResponse<IEnumerable<AnuncioResponse>> response = new BaseServiceResponse<IEnumerable<AnuncioResponse>>();
            if (!isValid)
            {
                response.Message = "No se pudo obtener la información del anuncio";
                return response;
            }
            response.Data = AnuncioFake.GetAnuncios();
            response.Message = "Se obtuvo existomante";
            response.Success = true;
            return await Task.FromResult(response);
        }

        private async Task<BaseServiceResponse<int>> GetCrearResponseAsync(bool isValid)
        {
            BaseServiceResponse<int> response = new BaseServiceResponse<int>();
            if (!isValid)
            {
                response.Message = "No se pudo registrar la información del anuncio";
                return response;
            }
            response.Data = 1;
            response.Message = "Se registró existomante";
            response.Success = true;
            return await Task.FromResult(response);
        }

        private async Task<BaseServiceResponse<bool>> GetEditarResponseAsync(bool isValid)
        {
            BaseServiceResponse<bool> response = new BaseServiceResponse<bool>();
            if (!isValid)
            {
                response.Message = "No se pudo editar la información del anuncio";
                return response;
            }
            response.Data = true;
            response.Message = "Se actualizó existomante";
            response.Success = true;
            return await Task.FromResult(response);
        }

        private async Task<BaseServiceResponse<bool>> GetEliminarResponseAsync(bool isValid)
        {
            BaseServiceResponse<bool> response = new BaseServiceResponse<bool>();
            if (!isValid)
            {
                response.Message = "No se pudo eliminar la información del anuncio";
                return response;
            }
            response.Data = true;
            response.Message = "Se eliminó existomante";
            response.Success = true;
            return await Task.FromResult(response);
        }
        #endregion

        public AnuncioControllerUnitTest()
        {
            _anuncioServiceMock = new Mock<IAnuncioService>();
            _usuarioServiceMock = new Mock<IUsuarioService>();

            ValidCreacionAnuncioRequest = AnuncioFake.GetCreacionAnuncioRequest();
            ValidEdicionAnuncioRequest = AnuncioFake.GetEdicionAnuncioRequest();
        }

        #region Success Paths
        [Test]
        public void Given_ValidUserId_When_ConsultarPorUsuarioAsync_Then_ReturnsBaseServiceResponseWithSuccess()
        {
            //Arrange
            _anuncioServiceMock.Setup(x => x.ConsultarPorUsuarioAsync(ValidOwnerUserId)).Returns(GetAnuncioResponseAsync(true));
            _usuarioServiceMock.Setup(x => x.ConsultarUsuarioAsync(ValidOwnerUserId)).Returns(GetUsuarioResponseAsync(true));

            //Act
            var anuncioController = new AnuncioController(_anuncioServiceMock.Object, _usuarioServiceMock.Object);
            var response = anuncioController.ConsultarAnunciosPorIdUsuario(ValidOwnerUserId);

            //Assert
            var result = response.Result as OkObjectResult;
            var baseServiceResponse = result.Value as BaseServiceResponse<IEnumerable<AnuncioResponse>>;
            Assert.IsTrue(baseServiceResponse.Success);
            Assert.IsTrue(baseServiceResponse.Data.ToList().Count > 0);
        }

        [Test]
        public void Given_ValidUserIdAndValidCreacionAnuncioRequest_When_CrearAsync_Then_ReturnsBaseServiceResponseWithSuccess()
        {
            //Arrange
            _anuncioServiceMock.Setup(x => x.CrearAsync(ValidOwnerUserId, ValidCreacionAnuncioRequest)).Returns(GetCrearResponseAsync(true));
            _usuarioServiceMock.Setup(x => x.ConsultarUsuarioAsync(ValidOwnerUserId)).Returns(GetUsuarioResponseAsync(true));

            //Act
            var anuncioController = new AnuncioController(_anuncioServiceMock.Object, _usuarioServiceMock.Object);
            var response = anuncioController.Crear(ValidOwnerUserId, ValidCreacionAnuncioRequest);

            //Assert
            var result = response.Result as CreatedResult;
            var baseServiceResponse = result.Value as BaseServiceResponse<int>;
            Assert.IsTrue(baseServiceResponse.Success);
            Assert.IsTrue(baseServiceResponse.Data != default);
            Assert.IsTrue(result.StatusCode == StatusCodes.Status201Created);
        }

        [Test]
        public void Given_ValidUserIdAndValidEdicionAnuncioRequest_When_EditarAsync_Then_ReturnsBaseServiceResponseWithSuccess()
        {
            //Arrange
            _anuncioServiceMock.Setup(x => x.EditarAsync(ValidOwnerUserId, ValidEdicionAnuncioRequest)).Returns(GetEditarResponseAsync(true));
            _usuarioServiceMock.Setup(x => x.ConsultarUsuarioAsync(ValidOwnerUserId)).Returns(GetUsuarioResponseAsync(true));

            //Act
            var anuncioController = new AnuncioController(_anuncioServiceMock.Object, _usuarioServiceMock.Object);
            var response = anuncioController.Editar(ValidOwnerUserId, ValidEdicionAnuncioRequest);

            //Assert
            var result = response.Result as OkObjectResult;
            var baseServiceResponse = result.Value as BaseServiceResponse<bool>;
            Assert.IsTrue(baseServiceResponse.Success);
            Assert.IsTrue(baseServiceResponse.Data);
        }

        [Test]
        public void Given_ValidUserIdAndValidAnuncioId_When_EliminarAsync_Then_ReturnsBaseServiceResponseWithSuccess()
        {
            //Arrange
            _anuncioServiceMock.Setup(x => x.EliminarAsync(ValidOwnerUserId, ValidAnuncioId)).Returns(GetEliminarResponseAsync(true));
            _usuarioServiceMock.Setup(x => x.ConsultarUsuarioAsync(ValidOwnerUserId)).Returns(GetUsuarioResponseAsync(true));

            //Act
            var anuncioController = new AnuncioController(_anuncioServiceMock.Object, _usuarioServiceMock.Object);
            var response = anuncioController.Eliminar(ValidOwnerUserId, ValidAnuncioId);

            //Assert
            var result = response.Result as OkObjectResult;
            var baseServiceResponse = result.Value as BaseServiceResponse<bool>;
            Assert.IsTrue(baseServiceResponse.Success);
            Assert.IsTrue(baseServiceResponse.Data);
        }
        #endregion

        #region Unsuccessful Paths
        [Test]
        public void Given_InvalidUserId_When_ConsultarPorUsuarioAsync_Then_ReturnsBaseServiceResponseWithSuccess()
        {
            //Arrange
            _anuncioServiceMock.Setup(x => x.ConsultarPorUsuarioAsync(InvalidOwnerUserId)).Returns(GetAnuncioResponseAsync(false));
            _usuarioServiceMock.Setup(x => x.ConsultarUsuarioAsync(InvalidOwnerUserId)).Returns(GetUsuarioResponseAsync(false));

            //Act
            var anuncioController = new AnuncioController(_anuncioServiceMock.Object, _usuarioServiceMock.Object);
            var response = anuncioController.ConsultarAnunciosPorIdUsuario(InvalidOwnerUserId);

            //Assert
            var result = response.Result as ForbidResult;
            Assert.IsNotNull(result.ToString());
        }

        [Test]
        public void Given_InvalidUserIdAndValidCreacionAnuncioRequest_When_CrearAsync_Then_ReturnsBaseServiceResponseWithSuccess()
        {
            //Arrange
            _anuncioServiceMock.Setup(x => x.CrearAsync(InvalidOwnerUserId, ValidCreacionAnuncioRequest)).Returns(GetCrearResponseAsync(false));
            _usuarioServiceMock.Setup(x => x.ConsultarUsuarioAsync(InvalidOwnerUserId)).Returns(GetUsuarioResponseAsync(false));

            //Act
            var anuncioController = new AnuncioController(_anuncioServiceMock.Object, _usuarioServiceMock.Object);
            var response = anuncioController.Crear(InvalidOwnerUserId, ValidCreacionAnuncioRequest);

            //Assert
            var result = response.Result as ForbidResult;
            Assert.IsNotNull(result.ToString());
        }

        [Test]
        public void Given_ValidUserIdAndInvalidCreacionAnuncioRequest_When_CrearAsync_Then_ReturnsBaseServiceResponseWithSuccess()
        {
            //Arrange
            _anuncioServiceMock.Setup(x => x.CrearAsync(ValidOwnerUserId, default)).Returns(GetCrearResponseAsync(false));
            _usuarioServiceMock.Setup(x => x.ConsultarUsuarioAsync(ValidOwnerUserId)).Returns(GetUsuarioResponseAsync(true));

            //Act
            var anuncioController = new AnuncioController(_anuncioServiceMock.Object, _usuarioServiceMock.Object);
            var response = anuncioController.Crear(ValidOwnerUserId, default);

            //Assert
            var result = response.Result as BadRequestObjectResult;
            Assert.IsNotNull(result.Value);
            Assert.IsTrue(result.StatusCode == StatusCodes.Status400BadRequest);
        }

        [Test]
        public void Given_InvalidUserIdAndValidEdicionAnuncioRequest_When_EditarAsync_Then_ReturnsBaseServiceResponseWithSuccess()
        {
            //Arrange
            _anuncioServiceMock.Setup(x => x.EditarAsync(InvalidOwnerUserId, ValidEdicionAnuncioRequest)).Returns(GetEditarResponseAsync(false));
            _usuarioServiceMock.Setup(x => x.ConsultarUsuarioAsync(InvalidOwnerUserId)).Returns(GetUsuarioResponseAsync(false));

            //Act
            var anuncioController = new AnuncioController(_anuncioServiceMock.Object, _usuarioServiceMock.Object);
            var response = anuncioController.Editar(InvalidOwnerUserId, ValidEdicionAnuncioRequest);

            //Assert
            var result = response.Result as ForbidResult;
            Assert.IsNotNull(result.ToString());
        }

        [Test]
        public void Given_ValidUserIdAndInvalidEdicionAnuncioRequest_When_EditarAsync_Then_ReturnsBaseServiceResponseWithSuccess()
        {
            //Arrange
            _anuncioServiceMock.Setup(x => x.EditarAsync(ValidOwnerUserId, default)).Returns(GetEditarResponseAsync(false));
            _usuarioServiceMock.Setup(x => x.ConsultarUsuarioAsync(ValidOwnerUserId)).Returns(GetUsuarioResponseAsync(true));

            //Act
            var anuncioController = new AnuncioController(_anuncioServiceMock.Object, _usuarioServiceMock.Object);
            var response = anuncioController.Editar(ValidOwnerUserId, default);

            //Assert
            var result = response.Result as BadRequestObjectResult;
            Assert.IsNotNull(result.Value);
            Assert.IsTrue(result.StatusCode == StatusCodes.Status400BadRequest);
        }

        [Test]
        public void Given_InvalidUserIdAndValidAnuncioId_When_EliminarAsync_Then_ReturnsBaseServiceResponseWithSuccess()
        {
            //Arrange
            _anuncioServiceMock.Setup(x => x.EliminarAsync(InvalidOwnerUserId, ValidAnuncioId)).Returns(GetEliminarResponseAsync(false));
            _usuarioServiceMock.Setup(x => x.ConsultarUsuarioAsync(InvalidOwnerUserId)).Returns(GetUsuarioResponseAsync(false));

            //Act
            var anuncioController = new AnuncioController(_anuncioServiceMock.Object, _usuarioServiceMock.Object);
            var response = anuncioController.Eliminar(InvalidOwnerUserId, ValidAnuncioId);

            //Assert
            var result = response.Result as ForbidResult;
            Assert.IsNotNull(result.ToString());
        }

        [Test]
        public void Given_ValidUserIdAndInvalidAnuncioId_When_EliminarAsync_Then_ReturnsBaseServiceResponseWithSuccess()
        {
            //Arrange
            _anuncioServiceMock.Setup(x => x.EliminarAsync(ValidOwnerUserId, InvalidAnuncioId)).Returns(GetEliminarResponseAsync(false));
            _usuarioServiceMock.Setup(x => x.ConsultarUsuarioAsync(ValidOwnerUserId)).Returns(GetUsuarioResponseAsync(true));

            //Act
            var anuncioController = new AnuncioController(_anuncioServiceMock.Object, _usuarioServiceMock.Object);
            var response = anuncioController.Eliminar(ValidOwnerUserId, InvalidAnuncioId);

            //Assert
            var result = response.Result as BadRequestObjectResult;
            Assert.IsNotNull(result.Value);
            Assert.IsTrue(result.StatusCode == StatusCodes.Status400BadRequest);
        }
        #endregion
    }
}
