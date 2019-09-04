using Film;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Film.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Text;
using Xunit.Abstractions;
using IntegrationTestFilm;

namespace asdasd
{

    public class UnitTest1 : IClassFixture<CustomWebApplicationFactory<Film.Startup>>
    {
        private readonly CustomWebApplicationFactory<Film.Startup> _factory;

        public UnitTest1(CustomWebApplicationFactory<Film.Startup> factory)
        {
            _factory = factory;
        }


        [Theory]
        [InlineData("/")]
        public async Task Test1(string url)
        {
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }
    }
}
