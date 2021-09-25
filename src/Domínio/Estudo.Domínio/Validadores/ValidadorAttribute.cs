using System;

namespace Estudo.Domínio.Validadores
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ValidadorAttribute<T> : Attribute
    {
        public ValidadorAttribute() => Tipo = typeof(T);

        public Type Tipo { get; init; }
    }
}
