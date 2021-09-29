using System.Collections.Generic;

namespace Estudo.Core.Domínio.Validação
{
    public interface INotificaçõesDoDomínio
    {
        bool PossuiNotificações();
        IEnumerable<NotificaçãoDoDomínio> ObterNotificações();
        void AdicionarNotificação(NotificaçãoDoDomínio notificaçãoDoDomínio);
    }
}
