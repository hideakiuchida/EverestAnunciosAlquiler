﻿using Everest.ViewModels;
using Everest.ViewModels.Request;
using Everest.ViewModels.Response;
using System.Threading.Tasks;

namespace Everest.Services.Interfaces
{
    public interface IPromocionService
    {
        Task<BaseServiceResponse<int>> GenerarPromocionAnuncioAsync();
        Task<BaseServiceResponse<PromocionAnuncioResponse>> ConsultarPromocionAsync(int idUsuario);
        Task<BaseServiceResponse<bool>> AgendarPromocionAnuncioAsync(int idUsuario, AgendarPromocionAnuncioRequest request);
    }
}