using Estudo.Cobranças.Aplicação.Armazenamento.Consumidores;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Cobranças.Serviço.Api.Consumidores
{
    public class ServiçoDeConsumidorDeClientes : BackgroundService
    {
        private readonly ConsumidorDeClientes consumidorDeClientes;

        public ServiçoDeConsumidorDeClientes(ConsumidorDeClientes consumidorDeClientes) =>
            this.consumidorDeClientes = consumidorDeClientes;

        protected override Task ExecuteAsync(CancellationToken stoppingToken) =>
              consumidorDeClientes.Iniciar(stoppingToken);
    }
}
