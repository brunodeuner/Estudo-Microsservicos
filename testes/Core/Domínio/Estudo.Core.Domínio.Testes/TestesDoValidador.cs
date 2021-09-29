using Estudo.Core.Domínio.Testes.Dtos;
using Estudo.Core.Domínio.Validadores;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Estudo.Core.Domínio.Testes
{
    public class TestesDoValidador
    {
        [Fact]
        public async Task Validar_ValidadorNãoSuportado_Exceção()
        {
            var validador = new Validador(default, default);
            var exceção = await Assert.ThrowsAsync<ArgumentException>(() =>
                validador.Validar(new ObjetoAValidar(), default).AsTask());
            Assert.Equal("ValidadorEmBranco não suportado!", exceção.Message);
        }
    }
}
