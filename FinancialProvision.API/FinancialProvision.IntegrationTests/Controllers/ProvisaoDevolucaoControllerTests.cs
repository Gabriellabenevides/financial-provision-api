using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;
using FinancialProvision.Provision.Application.DTOs;

namespace FinancialProvision.IntegrationTests.Controllers;

public class ProvisaoDevolucaoControllerTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ProvisaoDevolucaoControllerTests(
        CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }


    [Fact]
    public async Task POST_Deve_Criar_Registro()
    {
        var dto = new
        {
            mes = 1,
            ano = 2026,
            valorPrevisto = 1000,
            descricao = "teste criacao"
        };

        var response =
            await _client.PostAsJsonAsync(
                "/api/ProvisaoDevolucao",
                dto);

        response.StatusCode
            .Should()
            .Be(HttpStatusCode.Created);

        var created =
            await response.Content
            .ReadFromJsonAsync<ReadProvisaoDevolucaoDto>();

        created.Should().NotBeNull();

        created!.Mes.Should().Be(1);
        created.Ano.Should().Be(2026);
        created.ValorPrevisto.Should().Be(1000);
    }



    [Fact]
    public async Task GET_Deve_Listar_Registros()
    {
        var response =
            await _client.GetAsync(
                "/api/ProvisaoDevolucao");

        response.StatusCode
            .Should()
            .Be(HttpStatusCode.OK);

        var list =
            await response.Content
            .ReadFromJsonAsync<
                IEnumerable<ReadProvisaoDevolucaoDto>>();

        list.Should().NotBeNull();
    }



    [Fact]
    public async Task GET_BY_ID_Deve_Retornar_Registro()
    {
        var create = new
        {
            mes = 2,
            ano = 2026,
            valorPrevisto = 500,
            descricao = "teste get by id"
        };

        var post =
            await _client.PostAsJsonAsync(
                "/api/ProvisaoDevolucao",
                create);

        post.EnsureSuccessStatusCode();

        var created =
            await post.Content
            .ReadFromJsonAsync<ReadProvisaoDevolucaoDto>();


        var response =
            await _client.GetAsync(
                $"/api/ProvisaoDevolucao/{created!.Id}");

        response.StatusCode
            .Should()
            .Be(HttpStatusCode.OK);

        var entity =
            await response.Content
            .ReadFromJsonAsync<ReadProvisaoDevolucaoDto>();

        entity.Should().NotBeNull();

        entity!.Id.Should().Be(created.Id);
    }



    [Fact]
    public async Task PUT_Deve_Atualizar_Registro()
    {
        var create = new
        {
            mes = 3,
            ano = 2026,
            valorPrevisto = 700,
            descricao = "valor original"
        };

        var post =
            await _client.PostAsJsonAsync(
                "/api/ProvisaoDevolucao",
                create);

        post.EnsureSuccessStatusCode();

        var created =
            await post.Content
            .ReadFromJsonAsync<ReadProvisaoDevolucaoDto>();


        var update = new
        {
            valorPrevisto = 999,
            descricao = "valor atualizado"
        };

        var put =
            await _client.PutAsJsonAsync(
                $"/api/ProvisaoDevolucao/{created!.Id}",
                update);

        put.StatusCode
            .Should()
            .Be(HttpStatusCode.NoContent);


        var get =
            await _client.GetAsync(
                $"/api/ProvisaoDevolucao/{created.Id}");

        var updated =
            await get.Content
            .ReadFromJsonAsync<ReadProvisaoDevolucaoDto>();

        updated!.ValorPrevisto.Should().Be(999);
        updated.Descricao.Should().Be("valor atualizado");
    }



    [Fact]
    public async Task DELETE_Deve_Remover_Registro()
    {
        var create = new
        {
            mes = 4,
            ano = 2026,
            valorPrevisto = 300,
            descricao = "registro delete"
        };

        var post =
            await _client.PostAsJsonAsync(
                "/api/ProvisaoDevolucao",
                create);

        post.EnsureSuccessStatusCode();

        var created =
            await post.Content
            .ReadFromJsonAsync<ReadProvisaoDevolucaoDto>();


        var delete =
            await _client.DeleteAsync(
                $"/api/ProvisaoDevolucao/{created!.Id}");

        delete.StatusCode
            .Should()
            .Be(HttpStatusCode.NoContent);


        var get =
            await _client.GetAsync(
                $"/api/ProvisaoDevolucao/{created.Id}");

        get.StatusCode
            .Should()
            .Be(HttpStatusCode.NotFound);
    }
}