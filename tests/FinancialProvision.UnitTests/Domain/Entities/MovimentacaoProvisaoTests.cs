using FinancialProvision.Provision.Domain.Entities;

namespace FinancialProvision.UnitTests.Domain.Entities;
public class MovimentacaoProvisaoTests
{
    [Fact]
    public void Deve_Criar_Movimentacao_Com_Valores_Corretos()
    {
        // Arrange
        var provisaoId = 1;
        var valor = 100;
        var descricao = "Compra";

        // Act
        var movimentacao = new MovimentacaoProvisao(provisaoId, valor, descricao);

        // Assert
        Assert.Equal(provisaoId, movimentacao.ProvisaoDevolucaoId);
        Assert.Equal(valor, movimentacao.Valor);
        Assert.Equal(descricao, movimentacao.Descricao);
        Assert.NotEqual(default, movimentacao.DataCriacao);
    }

    [Fact]
    public void DataCriacao_Deve_Ser_Preenchida_Automaticamente()
    {
        // Arrange & Act
        var movimentacao = new MovimentacaoProvisao(1, 100, "teste");

        // Assert
        Assert.True(movimentacao.DataCriacao <= DateTime.UtcNow);
    }

    [Fact]
    public void Deve_Permitir_Descricao_Nula()
    {
        // Arrange & Act
        var movimentacao = new MovimentacaoProvisao(1, 100, null);

        // Assert
        Assert.Null(movimentacao.Descricao);
    }

    [Fact]
    public void Valor_Deve_Ser_Atribuido_Corretamente()
    {
        // Arrange
        var valor = 250;

        // Act
        var movimentacao = new MovimentacaoProvisao(1, valor, "teste");

        // Assert
        Assert.Equal(valor, movimentacao.Valor);
    }
}
