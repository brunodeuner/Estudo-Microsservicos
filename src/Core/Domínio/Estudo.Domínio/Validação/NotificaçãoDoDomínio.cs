using Estudo.Core.Domínio.Eventos;
using FluentValidation.Results;

namespace Estudo.Core.Domínio.Validação
{
    public class NotificaçãoDoDomínio : Evento
    {
        public NotificaçãoDoDomínio(ValidationResult resultadoDaValidação) =>
            ResultadoDaValidação = resultadoDaValidação;

        public NotificaçãoDoDomínio(string descrição) => Descrição = descrição;

        public string Descrição { get; }
        public ValidationResult ResultadoDaValidação { get; }
    }
}
