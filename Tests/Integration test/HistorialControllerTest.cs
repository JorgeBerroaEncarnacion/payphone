using Microsoft.AspNetCore.Mvc.Testing;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Integration_test;

public class HistorialControllerTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public HistorialControllerTest(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }


    [Fact]
    public async Task CreateHistorial_ShouldSucceed()
    {
        var postResponse = await _client.PostAsJsonAsync("/api/customer", new { Name = "History Customer" });
        var created = await postResponse.Content.ReadFromJsonAsync<Customer>();

        // Act
        var getResponse = await _client.GetAsync($"/api/customer/{created.Id}");
        var customer = await getResponse.Content.ReadFromJsonAsync<Customer>();

        var response = await _client.PostAsJsonAsync("/api/pedidos", new
        {
            CustomerId = customer.Id,
            TotalAmount = 999
        });
        var content = await response.Content.ReadAsStringAsync();
        var pedido = JsonSerializer.Deserialize<Pedido>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        var historialDto = new
        {
            OrderId = pedido.Id,
            PreviousStatus = Status.Pendiente,
            NewStatus = Status.Procesado
        };

        var newResponse = await _client.PostAsJsonAsync("/api/historial", historialDto);
        var newContent = await newResponse.Content.ReadAsStringAsync();

        Assert.True(newResponse.IsSuccessStatusCode, newContent);

        var historial = JsonSerializer.Deserialize<EstadosHistorial>(newContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        Assert.NotNull(historial);
        Assert.Equal(Status.Procesado, historial.NewStatus);
    }

    [Fact]
    public async Task GetHistorialByPedido_ShouldReturnHistory()
    {
        var postResponse = await _client.PostAsJsonAsync("/api/customer", new { Name = "Historial test" });
        var created = await postResponse.Content.ReadFromJsonAsync<Customer>();

        // Act
        var getResponse = await _client.GetAsync($"/api/customer/{created.Id}");
        var customer = await getResponse.Content.ReadFromJsonAsync<Customer>();

        var response = await _client.PostAsJsonAsync("/api/pedidos", new
        {
            CustomerId = customer.Id,
            TotalAmount = 1234
        });
        var content = await response.Content.ReadAsStringAsync();
        var pedido = JsonSerializer.Deserialize<Pedido>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });


        await _client.PostAsJsonAsync("/api/historial", new
        {
            OrderId = pedido.Id,
            PreviousStatus = Status.Pendiente,
            NewStatus = Status.Procesado
        });

        var responseHistorial = await _client.GetAsync($"/api/historial/{pedido.Id}");
        var history = await responseHistorial.Content.ReadFromJsonAsync<List<EstadosHistorial>>();

        Assert.NotNull(history);
        Assert.Single(history);
        Assert.Equal(Status.Procesado, history[0].NewStatus);
    }

}
