using Estudo.Domínio.Validação;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Domínio.Validadores
{
    public class Validador : IValidador
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IMediator mediator;

        public Validador(IServiceProvider serviceProvider, IMediator mediator)
        {
            this.serviceProvider = serviceProvider;
            this.mediator = mediator;
        }

        public async ValueTask<bool> Validar<T>(T objetoAValidar, CancellationToken cancellationToken)
        {
            var atributosDeValidador = objetoAValidar.GetType()
                .GetCustomAttributes(typeof(ValidadorAttribute), true)
                .Cast<ValidadorAttribute>();
            foreach (var atributoDeValidador in atributosDeValidador)
            {
                var validador = serviceProvider.GetRequiredService(atributoDeValidador.Tipo);
                if (validador is not IValidator<T> validadorFluentValidation)
                    throw new ArgumentException($"{validador.GetType().Name} não suportado!");
                var resultadoDaValidação = await validadorFluentValidation
                    .ValidateAsync(objetoAValidar, cancellationToken);
                if (!resultadoDaValidação.IsValid)
                {
                    await mediator.Publish(new NotificaçãoDoDomínio(resultadoDaValidação), cancellationToken);
                    return false;
                }
            }
            return true;
        }
    }
}
