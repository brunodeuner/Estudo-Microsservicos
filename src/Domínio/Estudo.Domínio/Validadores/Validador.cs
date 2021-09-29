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
                var validadorFluentValidation = ObterValidadorDoFluentValidation<T>(atributoDeValidador);
                if (!await ValidarObjeto(validadorFluentValidation, objetoAValidar, cancellationToken))
                    return false;
            }
            return true;
        }

        private IValidator<T> ObterValidadorDoFluentValidation<T>(ValidadorAttribute atributoDeValidador)
        {
            if (atributoDeValidador.Tipo.IsAssignableTo(typeof(IValidator<T>)))
                return (IValidator<T>)serviceProvider.GetRequiredService(atributoDeValidador.Tipo);
            throw new ArgumentException($"{atributoDeValidador.Tipo.Name} não suportado!");
        }

        private async ValueTask<bool> ValidarObjeto<T>(IValidator<T> validadorFluentValidation,
            T objetoAValidar, CancellationToken cancellationToken)
        {
            var resultadoDaValidação = await validadorFluentValidation
                .ValidateAsync(objetoAValidar, cancellationToken);
            if (resultadoDaValidação.IsValid)
                return true;

            await mediator.Publish(new NotificaçãoDoDomínio(resultadoDaValidação), cancellationToken);
            return false;
        }
    }
}
