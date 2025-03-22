using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Model.Models;
using System.Text.Json;
using DTO.Dto;
using Castle.Core.Resource;

namespace Tests.Integration_test;

public class PedidoControllerTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public PedidoControllerTest(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreatePedido_ShouldSucceed()
    {
        var postResponse = await _client.PostAsJsonAsync("/api/customer", new { Name = "Test User" });
        var created = await postResponse.Content.ReadFromJsonAsync<Customer>();

        // Act
        var getResponse = await _client.GetAsync($"/api/customer/{created.Id}");
        var customer = await getResponse.Content.ReadFromJsonAsync<Customer>();

        var pedidoDto = new
        {
            CustomerId = customer.Id,
            TotalAmount = 1500
        };

        var response = await _client.PostAsJsonAsync("/api/pedidos", pedidoDto);
        var content = await response.Content.ReadAsStringAsync();

        Assert.True(response.IsSuccessStatusCode, content);

        var pedido = JsonSerializer.Deserialize<Pedido>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        Assert.NotNull(pedido);
        Assert.Equal(1500, pedido.TotalAmount);
    }

    [Fact]
    public async Task UpdatePedido_ShouldChangeStatus()
    {
        var postResponse = await _client.PostAsJsonAsync("/api/customer", new { Name = "Test User" });
        var created = await postResponse.Content.ReadFromJsonAsync<Customer>();

        // Act
        var getResponse = await _client.GetAsync($"/api/customer/{created.Id}");
        var customer = await getResponse.Content.ReadFromJsonAsync<Customer>();

        var response = await _client.PostAsJsonAsync("/api/pedidos", new
        {
            CustomerId = customer.Id,
            TotalAmount = 500
        });
        var content = await response.Content.ReadAsStringAsync();
        var pedido = JsonSerializer.Deserialize<Pedido>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        var updateDto = new
        {
            Id = pedido.Id,
            CustomerId = customer.Id,
            TotalAmount = 100
        };

        var pedidoUpdated = await _client.PutAsJsonAsync($"/api/pedidos/{pedido.Id}", updateDto);
        var updated = await _client.GetFromJsonAsync<Pedido>($"/api/pedidos/{pedido.Id}");

        Assert.Equal(Status.Pendiente, updated.Status);
    }

    [Fact]
    public async Task DeletePedido_ShouldSucceed()
    {
        var postResponse = await _client.PostAsJsonAsync("/api/customer", new { Name = "Delete Owner" });
        var created = await postResponse.Content.ReadFromJsonAsync<Customer>();

        // Act
        var getResponse = await _client.GetAsync($"/api/customer/{created.Id}");
        var customer = await getResponse.Content.ReadFromJsonAsync<Customer>();

        var response = await _client.PostAsJsonAsync("/api/pedidos", new
        {
            CustomerId = customer.Id,
            TotalAmount = 800
        });
        var content = await response.Content.ReadAsStringAsync();
        var pedido = JsonSerializer.Deserialize<Pedido>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        var delete = await _client.DeleteAsync($"/api/pedidos/{pedido.Id}");

        Assert.True(delete.IsSuccessStatusCode);

        var get = await _client.GetAsync($"/api/pedidos/{pedido.Id}");
        Assert.Equal(System.Net.HttpStatusCode.NotFound, get.StatusCode);
    }
}
