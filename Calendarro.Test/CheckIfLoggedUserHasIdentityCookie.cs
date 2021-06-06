using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

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
        public void LoggedUser_StatusCode200_ReturnTrue()
        {
            _httpClient.BaseAddress = 
                new Uri("http://localhost:5000/Identity/Account/Login?ReturnUrl=%2F");
            _httpClient.DefaultRequestHeaders.Accept.Clear();

            // PROSZE UTWORZYC KONTO TESTOWE Z PODANYMI DANYMI!
            // BEDZIE TO KONTO DO SPRAWDZENIA POPRAWNOSCI FUNKCJONALNOSCI LOGOWANIA
            var login = "test@mail.com";
            var password = "Root1!";

            if (string.IsNullOrEmpty(login) | string.IsNullOrEmpty(password))
            {
                Assert.IsTrue(false, "Utworz wymagane testowe konto!!!.");
            }

            var formContent = new FormUrlEncodedContent(new[]
            {
                 new KeyValuePair<string, string>("Input.Email", login),
                 new KeyValuePair<string, string>("Input.Password", password),
            });

            var result = false;
            HttpResponseMessage responseMessage = null;

            try
            {
                responseMessage = _httpClient.PostAsync("", formContent).Result;
            }
            catch (Exception)
            {
                Assert.IsTrue(result, "Problem z wyslaniem requesta do servera.");
            }

            if (responseMessage == null)
            {
                Assert.IsTrue(result, "Nie otrzymano zadnej zwrotki z servera");
            }

            if (responseMessage?.StatusCode == HttpStatusCode.OK)
            {
                result = true;
            }

            Assert.IsTrue(result, "Status code nie jest 200! Blad.");
        }
    }
}
