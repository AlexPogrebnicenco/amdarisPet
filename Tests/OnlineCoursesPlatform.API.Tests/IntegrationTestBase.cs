//using System.Net.Http;
//using System.Text;
//using System.Text.Json;
//using Xunit;

//namespace OnlineCoursesPlatform.API.Tests
//{
//    public class IntegrationTestBase : IClassFixture<CustomWebApplicationFactory>
//    {
//        protected readonly HttpClient _client;
//        protected readonly CustomWebApplicationFactory Factory;

//        public IntegrationTestBase(CustomWebApplicationFactory factory)
//        {
//            Factory = factory; // сохраняем Factory
//            _client = Factory.CreateClient();
//        }

//        protected StringContent GetPayload(object obj)
//        {
//            return new StringContent(JsonSerializer.Serialize(obj), Encoding.UTF8, "application/json");
//        }
//    }
//}
