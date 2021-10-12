using Estudo.Core.Aplicação.Configurações;
using Estudo.Core.Domínio.Validação;
using Estudo.Core.Domínio.Validadores;
using Estudo.Core.Infraestrutura.Armazenamento.Abstrações;
using Estudo.Core.Infraestrutura.Armazenamento.HttpClient;
using Estudo.Core.Infraestrutura.Armazenamento.HttpClient.Queryable;
using Estudo.Core.Infraestrutura.Armazenamento.Memória;
using Estudo.Core.Infraestrutura.Armazenamento.Ravendb;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Estudo.Core.Aplicação
{
    public static class ConfigurarServiços
    {
        public static void ConfigurarAplicação(this IServiceCollection serviços,
            ConfiguraçãoDaAplicação configuraçãoDaAplicação)
        {
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("pt-BR");

            serviços.ConfigurarEscopo(configuraçãoDaAplicação.ObterAssemblies());
            serviços.ConfigurarArmazenamento(configuraçãoDaAplicação.ConfiguraçãoDaConexão);
            serviços.ConfigurarTransitórios(configuraçãoDaAplicação.ObterAssemblies());
        }

        public static T ObterConfiguração<T>(this IConfiguration configuração) =>
            configuração.GetSection(typeof(T).Name).Get<T>();

        private static void ConfigurarTransitórios(this IServiceCollection serviços, Assembly[] assemblies)
        {
            foreach (var repositorioASerInjetado in ObterTipoConcretosQueComeçamCom(assemblies, "ValidadorD"))
                serviços.AddTransient(repositorioASerInjetado);
        }

        private static void ConfigurarEscopo(this IServiceCollection serviços, Assembly[] assemblies)
        {
            serviços.InjetarRepositórios(assemblies);
            serviços.AddScoped<IValidador, Validador>();
            serviços.AddScoped<INotificaçõesDoDomínio, NotificaçõesDoDomínio>();
        }

        private static void InjetarRepositórios(this IServiceCollection serviços, Assembly[] assemblies)
        {
            serviços.AddMediatR(assemblies);
            foreach (var repositorioASerInjetado in ObterTipoConcretosQueComeçamCom(assemblies, "Repositório"))
                serviços.AddTransient(repositorioASerInjetado);
        }

        private static IEnumerable<Type> ObterTipoConcretosQueComeçamCom(Assembly[] assemblies,
            string começoDoNomeDoTipo) =>
            assemblies.SelectMany(x => x.GetTypes())
            .Where(x => !x.IsInterface && !x.IsAbstract && !x.IsNestedPrivate &&
                   x.FullName.Contains(começoDoNomeDoTipo, StringComparison.InvariantCultureIgnoreCase));

        private static void ConfigurarArmazenamento(this IServiceCollection serviços,
            ConfiguraçãoDaConexão configuraçãoDaConexão)
        {
            if (configuraçãoDaConexão is null)
                return;

            ConfigurarDaoRavendb(serviços, configuraçãoDaConexão);
            ConfigurarDaoMemória(serviços, configuraçãoDaConexão);
            ConfigurarDaoHttpClient(serviços, configuraçãoDaConexão);
        }

        private static void ConfigurarDaoRavendb(IServiceCollection serviços,
            ConfiguraçãoDaConexão configuraçãoDaConexão)
        {
            if (configuraçãoDaConexão.ObterTipo() != typeof(DaoRavendb))
                return;

            serviços.AddSingleton<FabricaDoRavendb>();
            serviços.AddSingleton(serviços =>
                serviços.GetRequiredService<FabricaDoRavendb>().DocumentStore);
            serviços.AddSingleton(configuraçãoDaConexão.ConfiguraçãoDoRavendb);
            serviços.AddScoped<IDao, DaoRavendb>();
        }

        private static void ConfigurarDaoMemória(IServiceCollection serviços,
            ConfiguraçãoDaConexão configuraçãoDaConexão)
        {
            if (configuraçãoDaConexão.ObterTipo() != typeof(DaoMemória))
                return;

            serviços.AddTransient<IDao, DaoMemória>();
        }

        private static void ConfigurarDaoHttpClient(IServiceCollection serviços,
            ConfiguraçãoDaConexão configuraçãoDaConexão)
        {
            if (configuraçãoDaConexão.ObterTipo() != typeof(DaoHttpClient))
                return;

            if (configuraçãoDaConexão.ConfiguraçãoDoDaoHttpClient.ConfigurarHttpClientPadrão)
                serviços.AddHttpClient();
            serviços.AddSingleton(configuraçãoDaConexão.ConfiguraçãoDoDaoHttpClient);
            serviços.AddTransient<ExecutorDeRequisições>();
            serviços.AddTransient<ExecutorExpressao>();
            serviços.AddTransient<IDao, DaoHttpClient>();
        }
    }
}
