using System;
using Xunit;

namespace Estudo.Infraestrutura.Armazenamento.Abstrações.Testes
{
    public class TestesParaExtensãoDeId
    {
        [Fact]
        public void ObterId_ObjetoSemPropriedadeId_Exceção()
        {
            var exceção = Assert.Throws<ArgumentException>(() => new object().ObterId());
            Assert.Equal("Object não possui propriedade Id que possa ser lida", exceção.Message);
        }

        [Fact]
        public void ObterId_ObjetoNulo_Exceção()
        {
            var exceção = Assert.Throws<ArgumentException>(() => default(object).ObterId());
            Assert.Equal(" não possui propriedade Id que possa ser lida", exceção.Message);
        }

        [Fact]
        public void AtribuirNovoId_ObjetoSemPropriedadeId_Exceção()
        {
            var exceção = Assert.Throws<ArgumentException>(() => new object().AtribuirNovoId());
            Assert.Equal("Object não possui propriedade Id que possa ser atribuida", exceção.Message);
        }

        [Fact]
        public void AtribuirNovoId_ObjetoNulo_Exceção()
        {
            var exceção = Assert.Throws<ArgumentException>(() => default(object).AtribuirNovoId());
            Assert.Equal(" não possui propriedade Id que possa ser atribuida", exceção.Message);
        }
    }
}
