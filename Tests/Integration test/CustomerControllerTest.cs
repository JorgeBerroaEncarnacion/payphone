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
using Microsoft.AspNetCore.Mvc.Testing;

namespace Tests.Integration_test;

public class CustomerControllerTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public CustomerControllerTest(WebApplicationFactory<Program> factory)
    {
        _client = factory.WithWebHostBuilder(builder =>{}).CreateClient();
    }

    [Fact]
    public async Task PostCustomer_ShouldReturnCreatedCustomer()
    {
        var dto = new { Name = "Integration Test" };
        var response = await _client.PostAsJsonAsync("/api/customer", dto);

        response.EnsureSuccessStatusCode();
        var customer = await response.Content.ReadFromJsonAsync<Customer>();
        Console.WriteLine("Raw response: " + customer);
        Assert.NotNull(customer);
        Assert.Equal("Integration Test", customer.Name);
    }

    [Fact]
    public async Task GetCustomerById_ShouldReturnCustomer()
    {
        // Arrange
        var postResponse = await _client.PostAsJsonAsync("/api/customer", new { Name = "Test User" });
        var created = await postResponse.Content.ReadFromJsonAsync<Customer>();

        // Act
        var getResponse = await _client.GetAsync($"/api/customer/{created.Id}");
        var customer = await getResponse.Content.ReadFromJsonAsync<Customer>();

        // Assert
        Assert.Equal(created.Id, customer.Id);
        Assert.Equal("Test User", customer.Name);
    }

}
