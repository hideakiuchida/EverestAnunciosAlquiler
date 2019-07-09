using Bogus;
using Everest.ViewModels.Response;
using System;
using System.Collections.Generic;

namespace Everest.UnitTests.Fakes
{
    public static class EvaluacionFake
    {
        public static List<EvaluacionResponse> GetEvaluaciones()
        {
            var randomValue = new Random().Next(1, 10);
            var ids = 0;
            var fake = new Faker<EvaluacionResponse>()
                .RuleFor(x => x.IdEvaluacion, f => ids++)
                .RuleFor(x => x.IdAnuncio, f => f.IndexFaker)
                .RuleFor(x => x.Calificacion, f => f.Random.Number(1, 5))
                .RuleFor(x => x.Comentario, f => f.Lorem.Sentence())
                .Generate(randomValue);
            return fake;
        }
    }
}
