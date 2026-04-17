using System.Text;

using Microsoft.AspNetCore.Mvc.Testing;

namespace Nrju.Pac;

/// <summary>
/// Integration tests.
/// </summary>
public class ProgramTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _webApplicationFactory;

    public ProgramTests(WebApplicationFactory<Program> webApplicationFactory)
    {
        _webApplicationFactory = webApplicationFactory;
    }

    [Fact]
    public async Task Get_Root_Returns_PAC_file_content()
    {
        var expectedContentType = "application/x-ns-proxy-autoconfig";
        var expectedContent = File.ReadAllText("default.pac", Encoding.UTF8);
        var expectedContentLength = expectedContent.Length;

        var client = _webApplicationFactory.CreateClient();
        var response = await client.GetAsync("/");

        var contentType = response.Content.Headers.ContentType?.MediaType;
        contentType.Should().Be(expectedContentType);

        var contentLength = response.Content.Headers.ContentLength;
        contentLength.Should().Be(expectedContentLength);

        var content = await response.Content.ReadAsStringAsync();
        content.Should().Be(expectedContent);
    }
}
