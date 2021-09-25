using MediatR;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Domínio.Validação
{
    internal class ManipuladorDeNotificaçõesDoDomínio : INotificationHandler<NotificaçãoDoDomínio>
    {
        private Collection<NotificaçãoDoDomínio> notificaçõesDoDomínio;

        public Task Handle(NotificaçãoDoDomínio notification, CancellationToken cancellationToken)
        {
            notificaçõesDoDomínio ??= new();
            notificaçõesDoDomínio.Add(notification);
            return Task.CompletedTask;
        }

        public IEnumerable<NotificaçãoDoDomínio> ObterNotificações() => notificaçõesDoDomínio;

        public bool PossuiNotificações() => notificaçõesDoDomínio.Any();
    }
}
