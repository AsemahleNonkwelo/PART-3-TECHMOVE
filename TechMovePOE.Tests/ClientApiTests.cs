using Microsoft.AspNetCore.Mvc.Testing;

namespace TechMovePOE.Tests;

public class ClientApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ClientApiTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public void Test_Project_Runs()
    {
        Assert.NotNull(_client);
    }
}