using MediatR;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Domínio.Validação
{
    public class ManipuladorDeNotificaçõesDoDomínio : INotificationHandler<NotificaçãoDoDomínio>,
        INotificaçõesDoDomínio
    {
        private ICollection<NotificaçãoDoDomínio> notificaçõesDoDomínio;

        public Task Handle(NotificaçãoDoDomínio notification, CancellationToken cancellationToken)
        {
            notificaçõesDoDomínio ??= new Collection<NotificaçãoDoDomínio>();
            notificaçõesDoDomínio.Add(notification);
            return Task.CompletedTask;
        }

        public bool PossuiNotificações() => notificaçõesDoDomínio?.Any() ?? false;

        public IEnumerable<NotificaçãoDoDomínio> ObterNotificações() => notificaçõesDoDomínio;
    }
}
