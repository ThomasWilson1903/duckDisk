using duckDisk.data.api.file.model;
using duckDisk.data.api.user.dto;
using Hanssens.Net;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Web;

namespace duckDisk.data.api.file
{
    internal class FileNetworkApi
    {
        private readonly HttpClient httpClient = new();
        private readonly LocalStorage localStorage = new();

        public Paging<FileModel> GetAll(int? folderId = null, int page = 0, int pageSize = 20)
        {
            var builder = new UriBuilder($"{NetwokConstants.BASE_URL}/files");

            var query = HttpUtility.ParseQueryString(builder.Query);
            query["folder_id"] = folderId.ToString();
            query["page"] = page.ToString();
            query["pageSize"] = pageSize.ToString();
            builder.Query = query.ToString();

            var url = builder.ToString();

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var response = httpClient.SendAsync(request).Result;

            var json = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<Paging<FileModel>>(json);
        }

        public FileModel Add(string fileName, string name, byte[] file, int? folderId = null)
        {
            var token = localStorage.Get<JwtResponseDto>("jwt_response").AccessToken;

            var requestContent = new MultipartFormDataContent();
            var fileContent = new ByteArrayContent(file);

            requestContent.Add(fileContent, name, fileName);
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            var response = httpClient.PatchAsync($"{NetwokConstants.BASE_URL}/files", requestContent).Result;

            var json = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<FileModel>(json);
        }
    }
}
