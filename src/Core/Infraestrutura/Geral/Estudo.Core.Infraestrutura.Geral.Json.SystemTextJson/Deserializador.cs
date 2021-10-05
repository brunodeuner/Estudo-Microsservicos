using Estudo.Core.Infraestrutura.Geral.Json.Abstrações;
using System;
using System.Text.Json;

namespace Estudo.Core.Infraestrutura.Geral.Json.SystemTextJson
{
    public class Deserializador : IDeserializador
    {
        public T Deserializar<T>(ReadOnlySpan<byte> bytes) => JsonSerializer.Deserialize<T>(bytes);
    }
}
