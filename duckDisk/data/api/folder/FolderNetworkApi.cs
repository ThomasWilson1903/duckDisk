using duckDisk.data.api.folder.model;
using Hanssens.Net;
using MaterialDesignColors;
using System.Data;
using System;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Windows.Controls;
using duckDisk.data.api.user.dto;
using duckDisk.data.api.file.model;
using System.Linq;

namespace duckDisk.data.api.folder
{
    internal class FolderNetworkApi
    {
        private readonly HttpClient httpClient = new();
        private readonly LocalStorage localStorage = new();

        public Paging<FolderModel> GetAll(int? folderId = null, int page = 0, int pageSize = 20)
        {
            var token = localStorage.Get<JwtResponseDto>("jwt_response").AccessToken;

            var builder = new UriBuilder($"{NetwokConstants.BASE_URL}/folders");

            var query = HttpUtility.ParseQueryString(builder.Query);
            query["folder_id"] = folderId.ToString();
            query["page"] = page.ToString();
            query["pageSize"] = pageSize.ToString();
            builder.Query = query.ToString();

            var url = builder.ToString();

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {token}");

            var response = httpClient.SendAsync(request).Result;

            var json = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<Paging<FolderModel>>(json);
        }

        public FolderModel Add(string name, int? folderId = null)
        {
            var token = localStorage.Get<JwtResponseDto>("jwt_response").AccessToken;

            var builder = new UriBuilder($"{NetwokConstants.BASE_URL}/folders");

            var query = HttpUtility.ParseQueryString(builder.Query);
            query["folder_id"] = folderId.ToString();
            query["name"] = name.ToString();
            builder.Query = query.ToString();

            var url = builder.ToString();

            var request = new HttpRequestMessage(HttpMethod.Post, url);

            request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {token}");

            var response = httpClient.SendAsync(request).Result;

            var json = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<FolderModel>(json);
        }

        public void Delete(int folderId)
        {
            var token = localStorage.Get<JwtResponseDto>("jwt_response").AccessToken;

            var builder = new UriBuilder($"{NetwokConstants.BASE_URL}/folders");

            var query = HttpUtility.ParseQueryString(builder.Query);
            query["folder_id"] = folderId.ToString();
            builder.Query = query.ToString();

            var url = builder.ToString();

            var request = new HttpRequestMessage(HttpMethod.Delete, url);

            request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {token}");

            var reponse = httpClient.SendAsync(request).Result;
        }

        public int? GetSize(int folderId)
        {
            var builder = new UriBuilder($"{NetwokConstants.BASE_URL}/folders/size");

            var query = HttpUtility.ParseQueryString(builder.Query);
            query["folder_id"] = folderId.ToString();
            builder.Query = query.ToString();

            var url = builder.ToString();

            var request = new HttpRequestMessage(HttpMethod.Head, url);

            var reponse = httpClient.SendAsync(request).Result;

            var folderSizeString = reponse.Headers.GetValues("folder_size").FirstOrDefault();

            if (folderSizeString == null)
                return null;
            else
                return int.Parse(folderSizeString);
        }

        public FolderModel IsPublickChange(int folderId)
        {
            var token = localStorage.Get<JwtResponseDto>("jwt_response").AccessToken;

            var builder = new UriBuilder($"{NetwokConstants.BASE_URL}/folders/public");

            var query = HttpUtility.ParseQueryString(builder.Query);
            query["folder_id"] = folderId.ToString();
            builder.Query = query.ToString();

            var url = builder.ToString();

            var request = new HttpRequestMessage(HttpMethod.Patch, url);

            request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {token}");

            var reponse = httpClient.SendAsync(request).Result;

            var json = reponse.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<FolderModel>(json);
        }

        public FolderModel Rename(int folderId, string newName)
        {
            var token = localStorage.Get<JwtResponseDto>("jwt_response").AccessToken;

            var builder = new UriBuilder($"{NetwokConstants.BASE_URL}/folders/rename");

            var query = HttpUtility.ParseQueryString(builder.Query);
            query["folder_id"] = folderId.ToString();
            query["name"] = newName.ToString();
            builder.Query = query.ToString();

            var url = builder.ToString();

            var request = new HttpRequestMessage(HttpMethod.Patch, url);

            request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {token}");

            var reponse = httpClient.SendAsync(request).Result;

            var json = reponse.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<FolderModel>(json);
        }
    }
}
