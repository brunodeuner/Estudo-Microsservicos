using Estudo.Domínio.Eventos;
using FluentValidation.Results;

namespace Estudo.Domínio.Validação
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
