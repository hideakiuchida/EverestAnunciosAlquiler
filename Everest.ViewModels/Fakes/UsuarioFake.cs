using Bogus;
using Everest.Common.Enums;
using Everest.ViewModels.Response;
using System;

namespace Everest.ViewModels.Fakes
{
    public static class UsuarioFake
    {
        public static UsuarioResponse GetUsuario(RolEnums rolEnum)
        {
            var fake = new Faker<UsuarioResponse>()
                .RuleFor(x => x.Identifier, f => (new Guid()).ToString())
                .RuleFor(x => x.Nombre, f => f.Name.FirstName())
                .RuleFor(x => x.Apellido, f => f.Name.LastName())
                .RuleFor(x => x.Correo, (f, u) => f.Internet.Email(u.Nombre, u.Apellido))
                .RuleFor(x => x.IdRol, f => (int)rolEnum)
                .Generate();
            return fake;
        }
    }
}
