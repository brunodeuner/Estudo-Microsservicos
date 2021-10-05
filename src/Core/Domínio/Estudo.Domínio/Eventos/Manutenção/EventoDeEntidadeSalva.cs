namespace Estudo.Core.Domínio.Eventos.Manutenção
{
    public class EventoDeEntidadeSalva<T> : Evento where T : new()
    {
        public EventoDeEntidadeSalva() { }

        public EventoDeEntidadeSalva(T entidade) => Entidade = entidade;

        public T Entidade { get; set; }
    }
}
