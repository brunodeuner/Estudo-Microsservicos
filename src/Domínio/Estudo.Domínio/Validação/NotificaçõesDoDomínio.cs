using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Estudo.Core.Domínio.Validação
{
    public class NotificaçõesDoDomínio : INotificaçõesDoDomínio
    {
        private ICollection<NotificaçãoDoDomínio> notificaçõesDoDomínio;

        public void AdicionarNotificação(NotificaçãoDoDomínio notificaçãoDoDomínio)
        {
            notificaçõesDoDomínio ??= new Collection<NotificaçãoDoDomínio>();
            notificaçõesDoDomínio.Add(notificaçãoDoDomínio);
        }

        public IEnumerable<NotificaçãoDoDomínio> ObterNotificações() => notificaçõesDoDomínio;

        public bool PossuiNotificações() => notificaçõesDoDomínio?.Any() ?? false;
    }
}
