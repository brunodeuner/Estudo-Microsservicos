using System;

namespace Estudo.Core.Infraestrutura.Geral.Json.Abstrações
{
    public interface IDeserializador
    {
        T Deserializar<T>(ReadOnlySpan<byte> bytes);
    }
}
