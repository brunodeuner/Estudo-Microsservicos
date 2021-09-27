using Estudo.Aplicação.Configurações;
using Estudo.Domínio.Validação;
using Estudo.Domínio.Validadores;
using Estudo.Infraestrutura.Armazenamento.Abstrações;
using Estudo.Infraestrutura.Armazenamento.Ravendb;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Estudo.Aplicação
{
    public static class ConfigurarServiços
    {
        public static void ConfigurarAplicação(this IServiceCollection serviços, IConfiguration configuração)
        {
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("pt-BR");

            var assemblies = configuração.ObterAssemblies();
            serviços.ConfigurarEscopo(assemblies);
            serviços.ConfigurarArmazenamento(configuração);
            serviços.ConfigurarTransitórios(assemblies);
        }

        private static void ConfigurarTransitórios(this IServiceCollection serviços, Assembly[] assemblies)
        {
            foreach (var repositorioASerInjetado in ObterTipoConcretosQueComeçamCom(assemblies, "ValidadorD"))
                serviços.AddTransient(repositorioASerInjetado);
        }

        private static Assembly[] ObterAssemblies(this IConfiguration configuração) =>
            configuração.ObterConfiguracao<AssembliesDaAplicação>().Assemblies.Select(Assembly.Load).ToArray();

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
                serviços.AddScoped(repositorioASerInjetado);
        }

        private static IEnumerable<Type> ObterTipoConcretosQueComeçamCom(Assembly[] assemblies, string começoDoNomeDoTipo) =>
            assemblies.SelectMany(x => x.GetTypes())
            .Where(x => !x.IsInterface && !x.IsAbstract && !x.IsNestedPrivate && x.FullName.Contains(começoDoNomeDoTipo,
                StringComparison.InvariantCultureIgnoreCase));

        private static void ConfigurarArmazenamento(this IServiceCollection serviços, IConfiguration configuração)
        {
            ConfigurarRavendbCasoPossuiaConfiguração(serviços, configuração);
        }

        private static void ConfigurarRavendbCasoPossuiaConfiguração(IServiceCollection serviços, IConfiguration configuração)
        {
            var configuraçãoDoRavendb = configuração.ObterConfiguracao<ConfiguraçãoDoRavendb>();
            if (configuraçãoDoRavendb is null)
                return;

            serviços.AddSingleton<FabricaDoRavendb>();
            serviços.AddSingleton(configuraçãoDoRavendb);
            serviços.AddScoped<IDao, DaoRavendb>();
        }

        private static T ObterConfiguracao<T>(this IConfiguration configuração) =>
            configuração.GetSection(typeof(T).Name).Get<T>();
    }
}
