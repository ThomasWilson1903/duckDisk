using duckDisk.data.api.folder.model;
using Hanssens.Net;
using MaterialDesignColors;
using System.Data;
using System;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace duckDisk.data.api.folder
{
    internal class FolderNetworkApi
    {
        private readonly HttpClient httpClient = new();
        private readonly LocalStorage localStorage = new();

        public Paging<Folder> GetAll(int? folderId = null, int page = 1, int pageSize = 20)
        {
            var builder = new UriBuilder($"{NetwokConstants.BASE_URL}/folders");

            var query = HttpUtility.ParseQueryString(builder.Query);
            query["folder_id"] = folderId.ToString();
            query["page"] = page.ToString();
            query["pageSize"] = pageSize.ToString();
            builder.Query = query.ToString();

            var url = builder.ToString();

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var response = httpClient.SendAsync(request).Result;

            var json = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<Paging<Folder>>(json);
        }
    }
}
