using duckDisk.data.api.user.dto;
using Hanssens.Net;
using System;
using System.Net.Http;

namespace duckDisk.data.api.basket
{
    internal class BasketNetworkApi
    {
        private readonly HttpClient httpClient = new();
        private readonly LocalStorage localStorage = new();

        public void DeleteAll()
        {
            var token = localStorage.Get<JwtResponseDto>("jwt_response").AccessToken;

            var builder = new UriBuilder($"{NetwokConstants.BASE_URL}/basket");

            var url = builder.ToString();

            var request = new HttpRequestMessage(HttpMethod.Delete, url);

            request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {token}");

            var reponse = httpClient.SendAsync(request).Result;
        }
    }
}
