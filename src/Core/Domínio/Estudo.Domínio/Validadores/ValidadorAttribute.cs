using System;

namespace Estudo.Core.Domínio.Validadores
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ValidadorAttribute : Attribute
    {
        public ValidadorAttribute(Type tipo) => Tipo = tipo;

        public Type Tipo { get; init; }
    }
}
