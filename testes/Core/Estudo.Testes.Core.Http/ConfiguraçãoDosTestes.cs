using Estudo.Testes.Core.Http.Dtos;
using System;
using System.Net.Http;

namespace Estudo.Testes.Core.Http
{
    public class ConfiguraçãoDosTestes
    {
        public ConfiguraçãoDosTestes(string diretorio, Func<Comando, HttpClient> fabricaHttpClient)
        {
            Diretorio = diretorio;
            FabricaHttpClient = fabricaHttpClient;
        }

        public string Diretorio { get; private set; }
        public Func<Comando, HttpClient> FabricaHttpClient { get; private set; }
    }
}
