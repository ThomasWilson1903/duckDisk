using duckDisk.data.api.file.model;
using duckDisk.data.api.folder.model;
using duckDisk.data.api.user.dto;
using Hanssens.Net;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Windows.Controls;

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

        public FileModel Add(string name, byte[] file, int? folderId = null)
        {
            var token = localStorage.Get<JwtResponseDto>("jwt_response").AccessToken;

            var builder = new UriBuilder($"{NetwokConstants.BASE_URL}/files");

            var query = HttpUtility.ParseQueryString(builder.Query);
            query["folder_id"] = folderId.ToString();
            builder.Query = query.ToString();

            var url = builder.ToString();

            var requestContent = new MultipartFormDataContent();
            var fileContent = new ByteArrayContent(file);

            requestContent.Add(fileContent, "file", name);
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            var response = httpClient.PostAsync(url, requestContent).Result;

            var json = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<FileModel>(json);
        }

        public void Delete(int filerId)
        {
            var token = localStorage.Get<JwtResponseDto>("jwt_response").AccessToken;

            var builder = new UriBuilder($"{NetwokConstants.BASE_URL}/files");

            var query = HttpUtility.ParseQueryString(builder.Query);
            query["file_id"] = filerId.ToString();
            builder.Query = query.ToString();

            var url = builder.ToString();

            var request = new HttpRequestMessage(HttpMethod.Delete, url);

            request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {token}");

            var reponse = httpClient.SendAsync(request).Result;
        }

        public FileModel Rename(int filerId, string newName)
        {
            var token = localStorage.Get<JwtResponseDto>("jwt_response").AccessToken;

            var builder = new UriBuilder($"{NetwokConstants.BASE_URL}/files");

            var query = HttpUtility.ParseQueryString(builder.Query);
            query["file_id"] = filerId.ToString();
            query["file_name"] = newName;
            builder.Query = query.ToString();

            var url = builder.ToString();

            var request = new HttpRequestMessage(HttpMethod.Patch, url);

            request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {token}");

            var response = httpClient.SendAsync(request).Result;

            var json = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<FileModel>(json);
        }

        public FileModel IsPublickChange(int fileId)
        {
            var token = localStorage.Get<JwtResponseDto>("jwt_response").AccessToken;

            var builder = new UriBuilder($"{NetwokConstants.BASE_URL}/files/public");

            var query = HttpUtility.ParseQueryString(builder.Query);
            query["file_id"] = fileId.ToString();
            builder.Query = query.ToString();

            var url = builder.ToString();

            var request = new HttpRequestMessage(HttpMethod.Patch, url);

            request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {token}");

            var reponse = httpClient.SendAsync(request).Result;

            var json = reponse.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<FileModel>(json);
        }

        public FileModel ChangeFolder(int fileId, int folderId)
        {
            var token = localStorage.Get<JwtResponseDto>("jwt_response").AccessToken;

            var builder = new UriBuilder($"{NetwokConstants.BASE_URL}/files/folder");

            var query = HttpUtility.ParseQueryString(builder.Query);
            query["file_id"] = fileId.ToString();
            query["folder_id"] = folderId.ToString();
            builder.Query = query.ToString();

            var url = builder.ToString();

            var request = new HttpRequestMessage(HttpMethod.Patch, url);

            request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {token}");

            var reponse = httpClient.SendAsync(request).Result;

            var json = reponse.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<FileModel>(json);
        }
    }
}
