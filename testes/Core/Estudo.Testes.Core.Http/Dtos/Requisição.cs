namespace Estudo.Core.Http.Testes.Dtos
{
    public class Requisição
    {
        public string Rota { get; set; }
        public string Método { get; set; }
        public object Corpo { get; set; }
    }
}
