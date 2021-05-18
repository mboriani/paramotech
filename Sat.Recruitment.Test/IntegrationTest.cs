using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using Sat.Recruitment.Api;
using Sat.Recruitment.Api.Data;
using Sat.Recruitment.Api.Model;

namespace Sat.Recruitment.Test
{
    public class IntegrationTest
    {
        protected readonly HttpClient testClient;

        public IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            testClient = appFactory.CreateClient();
        }

        public class JsonHttpContent<T> : StringContent
        {
            public JsonHttpContent(T content) : base(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json")
            {
            }
        }

        public Task<HttpResponseMessage> Post<T>(string url, T body)
        {
            var httpContent = new JsonHttpContent<T>(body);
            return testClient.PostAsync(url, httpContent);
        }
    }
}