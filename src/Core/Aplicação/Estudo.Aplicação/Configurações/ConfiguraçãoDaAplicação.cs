using System.Linq;
using System.Reflection;

namespace Estudo.Core.Aplicação.Configurações
{
    public class ConfiguraçãoDaAplicação
    {
        public string[] Assemblies { get; set; }
        public ConfiguraçãoDaConexão ConfiguraçãoDaConexão { get; set; }

        public Assembly[] ObterAssemblies() => Assemblies.Select(Assembly.Load).ToArray();
    }
}
