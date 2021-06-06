using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;

namespace Calendarro.Test
{
    [TestFixture]
    class CheckIfServerIsRunning
    {
        private HttpClient _httpClient;

        [SetUp]
        public void SetUp()
        {
            _httpClient = new HttpClient();
        }
        [Test]
        public void Server_StatusCode200_ReturnTrue()
        {
            var result = false;

            _httpClient.BaseAddress = new Uri("http://localhost:5000/Identity/Account/Login?ReturnUrl=%2F");
            _httpClient.DefaultRequestHeaders.Accept.Clear();

            HttpResponseMessage response = null;

            try
            {
                response = _httpClient.GetAsync("").Result;
            }
            catch (Exception)
            {
            }

            if (response?.StatusCode == HttpStatusCode.OK)
            {
                result = true;
            }

            Assert.IsTrue(result, "Server nie zostal uruchomiony.");
        }
    }
}
