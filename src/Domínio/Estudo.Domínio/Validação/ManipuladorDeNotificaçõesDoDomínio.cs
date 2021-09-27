using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Domínio.Validação
{
    public class ManipuladorDeNotificaçõesDoDomínio : INotificationHandler<NotificaçãoDoDomínio>
    {
        private readonly INotificaçõesDoDomínio notificaçõesDoDomínio;

        public ManipuladorDeNotificaçõesDoDomínio(INotificaçõesDoDomínio notificaçõesDoDomínio) =>
            this.notificaçõesDoDomínio = notificaçõesDoDomínio;

        public Task Handle(NotificaçãoDoDomínio notification, CancellationToken cancellationToken)
        {
            notificaçõesDoDomínio.AdicionarNotificação(notification);
            return Task.CompletedTask;
        }
    }
}
