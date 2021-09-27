using Estudo.Cobranças.Aplicação.Armazenamento.Consumidores;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Cobranças.Serviço.Api.Consumidores
{
    public class ServiçoDeConsumidorDeClientes : IHostedService
    {
        private readonly ConsumidorDeClientes consumidorDeClientes;

        public ServiçoDeConsumidorDeClientes(ConsumidorDeClientes consumidorDeClientes) =>
            this.consumidorDeClientes = consumidorDeClientes;

        public Task StartAsync(CancellationToken cancellationToken) =>
            consumidorDeClientes.Iniciar(cancellationToken);

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
