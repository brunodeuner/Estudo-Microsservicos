using Microsoft.AspNetCore.Mvc;

namespace Estudo.Core.Serviço.Api.Atributos
{
    public class RotaPadrãoAttribute : RouteAttribute
    {
        public RotaPadrãoAttribute() : base("[controller]") { }
    }
}
