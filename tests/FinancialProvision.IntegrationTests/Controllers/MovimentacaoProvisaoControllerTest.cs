using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FinancialProvision.Provision.Application.DTOs;

namespace FinancialProvision.IntegrationTests.Controllers;

public class MovimentacaoProvisaoControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public MovimentacaoProvisaoControllerTests(
        CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task POST_Deve_Criar_Movimentacao()
    {
        // cria provisão primeiro
        var provisao = new
        {
            mes = 1,
            ano = 2026,
            valorPrevisto = 1000,
            descricao = "teste"
        };

        var provResponse =
            await _client.PostAsJsonAsync(
                "/api/ProvisaoDevolucao",
                provisao);

        var createdProv =
            await provResponse.Content
            .ReadFromJsonAsync<ReadProvisaoDevolucaoDto>();


        var mov = new
        {
            provisaoDevolucaoId = createdProv!.Id,
            valor = 100,
            descricao = "compra"
        };

        var response =
            await _client.PostAsJsonAsync(
                "/api/MovimentacaoProvisao",
                mov);

        response.StatusCode
            .Should()
            .Be(HttpStatusCode.OK);
    }



    [Fact]
    public async Task GET_Deve_Retornar_Movimentacoes()
    {
        var provisao = new
        {
            mes = 2,
            ano = 2026,
            valorPrevisto = 1000,
            descricao = "teste"
        };

        var provResponse =
            await _client.PostAsJsonAsync(
                "/api/ProvisaoDevolucao",
                provisao);

        var createdProv =
            await provResponse.Content
            .ReadFromJsonAsync<ReadProvisaoDevolucaoDto>();


        await _client.PostAsJsonAsync(
            "/api/MovimentacaoProvisao",
            new
            {
                provisaoDevolucaoId = createdProv!.Id,
                valor = 50,
                descricao = "compra"
            });


        var response =
            await _client.GetAsync(
                $"/api/MovimentacaoProvisao/{createdProv.Id}");

        response.StatusCode
            .Should()
            .Be(HttpStatusCode.OK);

        var list =
            await response.Content
            .ReadFromJsonAsync<
                IEnumerable<ReadMovimentacaoDto>>();

        list.Should().NotBeNull();
        list.Should().NotBeEmpty();
    }



    [Fact]
    public async Task POST_Deve_Retornar_Erro_Quando_Provisao_Nao_Existe()
    {
        var mov = new
        {
            provisaoDevolucaoId = 999,
            valor = 100,
            descricao = "erro"
        };

        var response =
            await _client.PostAsJsonAsync(
                "/api/MovimentacaoProvisao",
                mov);

        response.StatusCode
            .Should()
            .Be(HttpStatusCode.BadRequest);
    }



    [Fact]
    public async Task Movimentacao_Deve_Atualizar_ValorUtilizado_Da_Provisao()
    {
        var provisao = new
        {
            mes = 3,
            ano = 2026,
            valorPrevisto = 1000,
            descricao = "teste"
        };

        var provResponse =
            await _client.PostAsJsonAsync(
                "/api/ProvisaoDevolucao",
                provisao);

        var createdProv =
            await provResponse.Content
            .ReadFromJsonAsync<ReadProvisaoDevolucaoDto>();


        await _client.PostAsJsonAsync(
            "/api/MovimentacaoProvisao",
            new
            {
                provisaoDevolucaoId = createdProv!.Id,
                valor = 200,
                descricao = "compra"
            });


        var get =
            await _client.GetAsync(
                $"/api/ProvisaoDevolucao/{createdProv.Id}");

        var updated =
            await get.Content
            .ReadFromJsonAsync<ReadProvisaoDevolucaoDto>();

        updated!.ValorUtilizado.Should().Be(200);
    }
}
