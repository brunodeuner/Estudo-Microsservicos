using Estudo.Cobranças.Domínio.Entidades;
using Estudo.Core.Domínio.Eventos;

namespace Estudo.Cobranças.Domínio.ManipuladoresDeEventos.Eventos
{
    public class EventoDePessoaCadastrada : Evento
    {
        public EventoDePessoaCadastrada(Pessoa pessoa) => Pessoa = pessoa;

        public Pessoa Pessoa { get; init; }
    }
}
