using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Everest.Common.Settings;
using Everest.Entities;
using Everest.Repository.Interfaces;
using Everest.Services.Interfaces;
using Everest.ViewModels;
using Everest.ViewModels.Request;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Everest.Services.Implementations
{
    public class ImagenService : IImagenService
    {
        private readonly IImagenRepository _imagenRepository;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;
        public ImagenService(IImagenRepository imagenRepository, IMapper mapper, IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _imagenRepository = imagenRepository;
            _mapper = mapper;
            _cloudinaryConfig = cloudinaryConfig;
            Account account = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(account);
        }
        public async Task<BaseServiceResponse<int>> CrearImagenAsync(int idAnuncio, CreacionImagenRequest request)
        {
            BaseServiceResponse<int> response = new BaseServiceResponse<int>();
            var imagenEntity = UploadingToCloudinary(request);

            if (string.IsNullOrEmpty(imagenEntity.ImagenUrl))
            {
                response.Message = "La imagen no se pudo subir a la nube.";
                return response;
            }
 
            imagenEntity.IdAnuncio = idAnuncio;
            var idImagen = await _imagenRepository.CrearImagenAsync(imagenEntity);

            if (idImagen == default)
            {
                response.Message = "La imagen no se pudo registrar.";
                return response;
            }

            response.Message = "La imagen se registró exitosamente.";
            response.Success = true;
            response.Data = idImagen;

            return response;
        }

        public async Task<BaseServiceResponse<bool>> EliminarAsync(int idAnuncio, int id)
        {
            BaseServiceResponse<bool> response = new BaseServiceResponse<bool>();
            var imagenEntity = await _imagenRepository.ConsultarAsync(id);

            if (imagenEntity is null)
            {
                response.Message = $"No se pudo encontrar la foto con id {id}";
                return response;
            }

            var eliminado = await EliminarImagenCloudinary(imagenEntity);
            if (!eliminado)
            {
                response.Message = $"No se pudo eliminar la foto con id {id} en la nube";
                return response;
            }

            response.Message = "Se eliminó exitosamente.";
            response.Success = eliminado;
            response.Data = eliminado;

            return response;
        }

        #region Privates Methods
        private ImagenEntity UploadingToCloudinary(CreacionImagenRequest request)
        {
            var file = request.Imagen;
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
                    };
                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }
            var imagenEntity = _mapper.Map<ImagenEntity>(request);
            imagenEntity.ImagenUrl = uploadResult.Uri.ToString();
            imagenEntity.IdPublico = uploadResult.PublicId;
            return imagenEntity;
        }

        private async Task<bool> EliminarImagenCloudinary(ImagenEntity imagenEntity)
        {
            if (imagenEntity.IdPublico != null)
            {
                var deleteParams = new DeletionParams(imagenEntity.IdPublico);
                var result = _cloudinary.Destroy(deleteParams);

                if (result.Result != "ok")
                    return false;
            }

            return await _imagenRepository.EliminarImagenAsync(imagenEntity.IdImagen);
        }
        #endregion
    }
}
