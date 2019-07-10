using Bogus;
using Everest.ViewModels.Response;
using System;
using System.Collections.Generic;

namespace Everest.ViewModels.Fakes
{
    public static class ImagenFake
    {
        public static List<ImagenResponse> GetImagenes()
        {
            var randomValue = new Random().Next(1, 10);
            var ids = 0;
            var fake = new Faker<ImagenResponse>()
                .RuleFor(x => x.IdImagen, f => ids++)
                .RuleFor(x => x.Descripcion, f => f.Lorem.Text())
                .RuleFor(x => x.ImagenUrl, f => "http//:image..com/image1.png")
                .Generate(randomValue);
            return fake;
        }
    }
}
