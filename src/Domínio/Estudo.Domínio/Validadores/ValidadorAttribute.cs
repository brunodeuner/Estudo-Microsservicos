using System;

namespace Estudo.Domínio.Validadores
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ValidadorAttribute : Attribute
    {
        public ValidadorAttribute(Type tipo) => Tipo = tipo;

        public Type Tipo { get; private set; }
    }
}
