namespace Estudo.Core.Domínio.Eventos.Manutenção
{
    public class EventoDeEntidadeRemovida<T> : Evento where T : new()
    {
        public EventoDeEntidadeRemovida() { }

        public EventoDeEntidadeRemovida(T entidade) => Entidade = entidade;

        public T Entidade { get; set; }
    }
}
