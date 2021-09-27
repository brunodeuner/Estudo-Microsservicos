namespace Estudo.Testes.Core.Http.Dtos
{
    public class Requisição
    {
        public string Rota { get; set; }
        public string Método { get; set; }
        public object Corpo { get; set; }
    }
}
