using Estudo.Cobranças.Domínio.Consultas.Entidades;
using Estudo.Cobranças.Domínio.Consultas.Repositórios;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;

namespace Estudo.Cobranças.Serviço.Api.Controllers
{
    [Route("[controller]")]
    public class CobrançasPorMêsEEstadoController
    {
        private readonly RepositórioDeCobrançasPorMêsEEstado repositórioDeCobrançasPorMêsEEstado;

        public CobrançasPorMêsEEstadoController(
            RepositórioDeCobrançasPorMêsEEstado repositórioDeCobrançasPorMêsEEstado) =>
            this.repositórioDeCobrançasPorMêsEEstado = repositórioDeCobrançasPorMêsEEstado;

        [HttpGet]
        public IAsyncEnumerable<CobrançasPorMêsEEstado> ObterTodasAsCobrançasPorMêsEEstado(
            CancellationToken cancellationToken) => repositórioDeCobrançasPorMêsEEstado.ObterTodas(cancellationToken);
    }
}
