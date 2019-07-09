﻿using Bogus;
using Everest.Common.Enums;
using Everest.Entities;
using Everest.ViewModels.Request;
using Everest.ViewModels.Response;
using System.Collections.Generic;

namespace Everest.UnitTests.Fakes
{
    public static class AnuncioFake
    {
        public static List<AnuncioResponse> GetAnuncios()
        {
            var anuncioIds = 0;
            var fake = new Faker<AnuncioResponse>()
                .RuleFor(x => x.IdAnuncio, f => anuncioIds++)
                .RuleFor(x => x.Activo, f => f.PickRandom(new bool[] { true, false }))
                .RuleFor(x => x.AdmiteMascota, f => f.PickRandom(new bool[] { true, false }))
                .RuleFor(x => x.CantidadBaños, f => f.Random.Number(1, 20))
                .RuleFor(x => x.CantidadHabitaciones, f => f.Random.Number(1, 20))
                .RuleFor(x => x.CantidadParqueos, f => f.Random.Number(1, 20))
                .RuleFor(x => x.MaximaCantidadPersonas, f => f.Random.Number(1, 20))
                .RuleFor(x => x.Direccion, f => f.Address.StreetAddress())
                .RuleFor(x => x.FechaCreacion, f => f.Date.Recent())
                .RuleFor(x => x.Latitud, f => (decimal)f.Address.Latitude())
                .RuleFor(x => x.Longitud, f => (decimal)f.Address.Longitude())
                .RuleFor(x => x.Metros2, f => f.Random.Number(70, 500))
                .RuleFor(x => x.Plantas, f => f.Random.Number(1, 5))
                .RuleFor(x => x.Precio, f => f.Random.Decimal(100000, 1000000))
                .RuleFor(x => x.TieneSeguridadPrivada, f => f.PickRandom(new bool[] { true, false }))
                .RuleFor(x => x.Evaluaciones, EvaluacionFake.GetEvaluaciones)
                .RuleFor(x => x.Imagenes, ImagenFake.GetImagenes)
                .RuleFor(x => x.TipoPropiedad, TipoPropiedadFake.GetTipoPropiedad)
                .RuleFor(x => x.Usuario, UsuarioFake.GetUsuario(RolEnums.Propietario))
                .Generate(10);
            return fake;
        }

        public static CreacionAnuncioRequest GetCreacionAnuncioRequest()
        {
            var ids = 0;
            var fake = new Faker<CreacionAnuncioRequest>()
                .RuleFor(x => x.IdUsuario, f => ids++)
                .RuleFor(x => x.AdmiteMascota, f => f.PickRandom(new bool[] { true, false}))
                .RuleFor(x => x.CantidadBaños, f => f.Random.Number(1, 20))
                .RuleFor(x => x.CantidadHabitaciones, f => f.Random.Number(1, 20))
                .RuleFor(x => x.CantidadParqueos, f => f.Random.Number(1, 20))
                .RuleFor(x => x.MaximaCantidadPersonas, f => f.Random.Number(1, 20))
                .RuleFor(x => x.Direccion, f => f.Address.StreetAddress())
                .RuleFor(x => x.Latitud, f => (decimal)f.Address.Latitude())
                .RuleFor(x => x.Longitud, f => (decimal)f.Address.Longitude())
                .RuleFor(x => x.Metros2, f => f.Random.Number(70, 500))
                .RuleFor(x => x.Plantas, f => f.Random.Number(1, 5))
                .RuleFor(x => x.Precio, f => f.Random.Decimal(100000, 1000000))
                .RuleFor(x => x.TieneSeguridadPrivada, f => f.PickRandom(new bool[] { true, false }))
                .RuleFor(x => x.IdTipoPropiedad, f => f.Random.Number(1, 6))
                .Generate();
            return fake;
        }

        public static EdicionAnuncioRequest GetEdicionAnuncioRequest()
        {
            var ids = 0;
            var fake = new Faker<EdicionAnuncioRequest>()
                .RuleFor(x => x.IdUsuario, f => ids++)
                .RuleFor(x => x.AdmiteMascota, f => f.PickRandom(new bool[] { true, false }))
                .RuleFor(x => x.CantidadBaños, f => f.Random.Number(1, 20))
                .RuleFor(x => x.CantidadHabitaciones, f => f.Random.Number(1, 20))
                .RuleFor(x => x.CantidadParqueos, f => f.Random.Number(1, 20))
                .RuleFor(x => x.MaximaCantidadPersonas, f => f.Random.Number(1, 20))
                .RuleFor(x => x.Direccion, f => f.Address.StreetAddress())
                .RuleFor(x => x.Latitud, f => (decimal)f.Address.Latitude())
                .RuleFor(x => x.Longitud, f => (decimal)f.Address.Longitude())
                .RuleFor(x => x.Metros2, f => f.Random.Number(70, 500))
                .RuleFor(x => x.Plantas, f => f.Random.Number(1, 5))
                .RuleFor(x => x.Precio, f => f.Random.Decimal(100000, 1000000))
                .RuleFor(x => x.TieneSeguridadPrivada, f => f.PickRandom(new bool[] { true, false }))
                .RuleFor(x => x.IdTipoPropiedad, f => f.Random.Number(1, 6))
                .RuleFor(x => x.IdAnuncio, f => f.Random.Number(1, 6))
                .RuleFor(x => x.Activo, f => true)
                .Generate();
            return fake;
        }
    }
}