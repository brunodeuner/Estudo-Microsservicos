using Estudo.Core.Domínio.Validadores;

namespace Estudo.Core.Domínio.Testes.Dtos
{
    [Validador(typeof(ValidadorEmBranco))]
    internal class ObjetoAValidar { }
}
