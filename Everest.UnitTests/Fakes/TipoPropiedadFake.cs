using Bogus;
using Everest.ViewModels.Response;

namespace Everest.UnitTests.Fakes
{
    public static class TipoPropiedadFake
    {
        public static TipoPropiedadResponse GetTipoPropiedad()
        {
            var ids = 0;
            var fake = new Faker<TipoPropiedadResponse>()
                .RuleFor(x => x.IdTipoPropiedad, f => ids++)
                .RuleFor(x => x.Nombre, f => f.PickRandomParam(new string[] { "Habitación", "Apartamento"}))
                .Generate();
            return fake;
        }
    }
}
