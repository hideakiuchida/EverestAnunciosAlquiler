using Everest.ViewModels;
using Everest.ViewModels.Request;
using Everest.ViewModels.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Everest.Services.Interfaces
{
    public interface IPromocionService
    {
        Task<BaseServiceResponse<int>> GenerarPromocionAnuncioAsync();
        Task<BaseServiceResponse<PromocionAnuncioResponse>> ConsultarPromocionAsync();
        Task<BaseServiceResponse<bool>> AgendarPromocionAnuncioAsync(AgendarPromocionAnuncioRequest request);
    }
}
