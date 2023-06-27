using duckDisk.data.api.user.dto;
using Hanssens.Net;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Json;

namespace duckDisk.data.api.user
{
    internal class UserNetworkApi
    {
        private readonly HttpClient httpClient = new();
        private readonly LocalStorage localStorage = new();

        public JwtResponseDto? GetLocalJwtResponse()
        {
            try
            {
                return localStorage.Get<JwtResponseDto>("jwt_response");
            }catch (Exception)
            {
                return null;
            }
        }

        public void SignOut()
        {
            localStorage.Clear();
            localStorage.Dispose();
        }

        public bool IsAuth()
        {
            try
            {
                var token = localStorage.Get<JwtResponseDto>("jwt_response");

                if (token == null)
                    return false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public JwtResponseDto Login(JwtRequestDto body)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{NetwokConstants.BASE_URL}/users/login")
            {
                Content = JsonContent.Create(body)
            };

            var response = httpClient.SendAsync(request).Result;

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<JwtResponseDto>(json);

            localStorage.Store("jwt_response", result);
            localStorage.Dispose();

            return result;
        }

        public JwtResponseDto Register(JwtRequestDto body)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{NetwokConstants.BASE_URL}/users/register")
            {
                Content = JsonContent.Create(body)
            };

            var response = httpClient.SendAsync(request).Result;

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<JwtResponseDto>(json);

            localStorage.Store("jwt_response", result);
            localStorage.Dispose();

            return result;
        }
    }
}
