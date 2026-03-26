using FinancialProvision.Provision.Domain.Entities;

namespace FinancialProvision.UnitTests.Domain.Entities;

public class ProvisaoDevolucaoTests
{
    [Fact]
    public void Deve_Criar_Provisao_Com_Valores_Corretos()
    {
        // Arrange
        var mes = 1;
        var ano = 2026;
        var valorPrevisto = 1000;
        var descricao = "teste";

        // Act
        var provisao = new ProvisaoDevolucao(mes, ano, valorPrevisto, descricao);

        // Assert
        Assert.Equal(mes, provisao.Mes);
        Assert.Equal(ano, provisao.Ano);
        Assert.Equal(valorPrevisto, provisao.ValorPrevisto);
        Assert.Equal(0, provisao.ValorUtilizado);
        Assert.Equal(descricao, provisao.Descricao);
        Assert.NotEqual(default, provisao.DataCriacao);
    }

    [Fact]
    public void Deve_Atualizar_Valor_Previsto()
    {
        // Arrange
        var provisao = new ProvisaoDevolucao(1, 2026, 1000, "teste");

        // Act
        provisao.AtualizarValor(2000, "novo valor");

        // Assert
        Assert.Equal(2000, provisao.ValorPrevisto);
        Assert.Equal("novo valor", provisao.Descricao);
    }

    [Fact]
    public void Deve_Somar_Valor_Utilizado()
    {
        // Arrange
        var provisao = new ProvisaoDevolucao(1, 2026, 1000, "teste");

        // Act
        provisao.RegistrarUtilizacao(100);
        provisao.RegistrarUtilizacao(50);

        // Assert
        Assert.Equal(150, provisao.ValorUtilizado);
    }

    [Fact]
    public void DataCriacao_Deve_Ser_Preenchida_Automaticamente()
    {
        // Arrange & Act
        var provisao = new ProvisaoDevolucao(1, 2026, 1000, "teste");

        // Assert
        Assert.True(provisao.DataCriacao <= DateTime.UtcNow);
    }
}