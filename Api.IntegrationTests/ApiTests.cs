using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;

namespace Tests
{
    // dotnet add package Microsoft.AspNetCore.TestHost
    // dotnet add package Microsoft.AspNetCore.App
    public class ApiTests
    {
        private TestServer server;
        private HttpClient client;

        [SetUp]
        public void Setup()
        {
            var builder = new WebHostBuilder()
                .UseStartup<Api.Startup>()
                .UseEnvironment("Development");

            server = new TestServer(builder);

            client = server.CreateClient();
        }


        [Test]
        public async Task Get_Index_ReturnHelloWorld()
        {
            // Act
            var response = await client.GetAsync("api/tracking");

            var result = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.That(result, Is.EqualTo("Hello World"));


        }

    }
}