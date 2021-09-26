using System.Collections.Generic;

namespace Estudo.Domínio.Validação
{
    public interface INotificaçõesDoDomínio
    {
        bool PossuiNotificações();
        IEnumerable<NotificaçãoDoDomínio> ObterNotificações();
    }
}
