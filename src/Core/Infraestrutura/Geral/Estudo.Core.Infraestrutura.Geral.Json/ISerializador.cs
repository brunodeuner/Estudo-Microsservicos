namespace Estudo.Core.Infraestrutura.Geral.Json.Abstrações
{
    public interface ISerializador
    {
        byte[] Serializar(object objeto);
    }
}
