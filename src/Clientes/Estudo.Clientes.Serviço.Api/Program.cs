﻿using Estudo.Serviço.Api;
using System.Threading.Tasks;

namespace Estudo.Clientes.Serviço.Api
{
    internal static class Program
    {
        public static async Task Main(string[] args) => await CriarHostBuilder.CriarERodar<Startup>(args);
    }
}
