using Estudo.Cobranças.Aplicação.Armazenamento.Clientes.Dtos;
using Estudo.Cobranças.Domínio.Entidades;
using Estudo.Cobranças.Domínio.Repositórios;
using Estudo.Infraestrutura.Armazenamento.Abstrações;
using Estudo.Infraestrutura.Bus.Abstrações.Consumidor;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Cobranças.Aplicação.Armazenamento.Clientes
{
    public class ConsumidorDeClientes
    {
        private readonly IConsumidor<Cliente> consumidor;
        private readonly IServiceProvider serviceProvider;

        public ConsumidorDeClientes(IConsumidor<Cliente> consumidor, IServiceProvider serviceProvider)
        {
            this.consumidor = consumidor;
            this.serviceProvider = serviceProvider;
        }

        public Task Iniciar(CancellationToken cancellationToken)
        {
            consumidor.Consumir += async (requisição, cancellationToken) =>
            {
                using var escopo = serviceProvider.CreateScope();
                var serviceProviderDoEscopo = escopo.ServiceProvider;
                var repositórioDePessoa = serviceProviderDoEscopo.GetRequiredService<RepositórioDePessoa>();
                await repositórioDePessoa.Salvar(new Pessoa(requisição.Corpo.Cpf), cancellationToken);
                var dao = serviceProviderDoEscopo.GetRequiredService<IDao>();
                await dao.SalvarAlterações(cancellationToken);
            };
            return consumidor.Iniciar(nameof(Cliente), cancellationToken);
        }
    }
}
