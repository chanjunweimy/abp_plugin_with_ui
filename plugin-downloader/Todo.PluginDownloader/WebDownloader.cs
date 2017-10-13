using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json.Linq;
using Todo.MainProject.Communication.Dto;

namespace Todo.PluginDownloader
{
    public class WebDownloader
    {
        private readonly HttpClient _httpClient;

        public WebDownloader(string hostUrl)
        {
            _httpClient = InitializeClient(hostUrl);
        }

        public async Task<List<PluginObject>> GetPluginObjects(string path)
        {
            var pluginObjects = new List<PluginObject>();
            var response = await _httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var obj = JObject.Parse(responseString);
                pluginObjects = obj["result"].ToObject<List<PluginObject>>();
            }
            return pluginObjects;
        }

        public async Task<List<FileObject>> GetFileContentResults(string path, string pluginName)
        {
            var fileContentResults = new List<FileObject>();
            path = QueryHelpers.AddQueryString(path, "pluginName", pluginName);
            var response = await _httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var obj = JObject.Parse(responseString);
                fileContentResults = obj["result"].ToObject<List<FileObject>>();
            }
            return fileContentResults;
        }

        private HttpClient InitializeClient(string hostUrl)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(hostUrl)
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }
}
