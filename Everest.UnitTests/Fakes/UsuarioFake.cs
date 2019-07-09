using Bogus;
using Everest.Common.Enums;
using Everest.ViewModels.Response;

namespace Everest.UnitTests.Fakes
{
    public static class UsuarioFake
    {
        public static UsuarioResponse GetUsuario(RolEnums rolEnum)
        {
            var ids = 0;
            var fake = new Faker<UsuarioResponse>()
                .RuleFor(x => x.IdUsuario, f => ids++)
                .RuleFor(x => x.Nombre, f => f.Name.FirstName())
                .RuleFor(x => x.Apellido, f => f.Name.LastName())
                .RuleFor(x => x.Correo, (f, u) => f.Internet.Email(u.Nombre, u.Apellido))
                .RuleFor(x => x.IdRol, f => (int)rolEnum)
                .Generate();
            return fake;
        }
    }
}
