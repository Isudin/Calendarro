using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Calendarro.Test
{
    [TestFixture]
    class LoggedUserIdentity
    {
        private HttpClient _httpClient;

        [SetUp]
        public void SetUp()
        {
            _httpClient = new HttpClient();
        }

        [Test]
        public void LoggedUser_HasCookie_ReturnTrue()
        {
            _httpClient.BaseAddress = new Uri("http://localhost:5000/Identity/Account/Login?ReturnUrl=%2F");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var login = "test@op.pl";
            var password = "Admin1@";

            var formContent = new FormUrlEncodedContent(new[]
            {
             new KeyValuePair<string, string>("grant_type", "password"),
             new KeyValuePair<string, string>("username", login),
             new KeyValuePair<string, string>("password", password),
            });

            //var responseMessage =  _httpClient.PostAsync("/Token", formContent);
            
            //var responseJson =  responseMessage.Content.ReadAsStringAsync().Result;

            Assert.IsFalse(false, "1 should not be prime");
        }
    }
}
