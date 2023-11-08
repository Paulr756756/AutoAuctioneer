using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using auc_client.Models.DTOs;
using Blazored.LocalStorage;

namespace auc_client.Shared; 

public class GenericMethods {
    private readonly ILocalStorageService _localStorageService;
    private readonly HttpClient _httpClient;

    public GenericMethods(ILocalStorageService localStorageService, HttpClient httpClient) {
        _localStorageService = localStorageService;
        _httpClient = httpClient;
    }

    public async Task<List<T>?> GetValuesFromApi<T>(string url) where T : class {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Bearer",
            (await _localStorageService.GetItemAsStringAsync("token")).Trim('"').Trim());
        var responseObject = await _httpClient.GetFromJsonAsync<List<T>>(EnvUrls.BaseUrl + url);
        return responseObject;
    }
    public async Task<HttpResponseMessage> PostValuesToApi<T>(string url,T item) where T: class {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Bearer",
            (await _localStorageService.GetItemAsStringAsync("token")).Trim('"').Trim()
        );
        var responseObject = await _httpClient.PostAsJsonAsync(EnvUrls.BaseUrl+url, item);
        return responseObject;
    }
    public async Task<HttpResponseMessage> DeleteValuesFromApi<T>(string url, T item) where T:class {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Bearer",
            (await _localStorageService.GetItemAsStringAsync("token")).Trim('"').Trim());
        //var responseObject = await _httpClient.DeleteFromJsonAsync(EnvUrls.BaseUrl + url, typeof(bool), item);
        var uri = Path.Combine(EnvUrls.BaseUrl, url);
        var json = JsonSerializer.Serialize(item);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var request = new HttpRequestMessage(HttpMethod.Delete, uri);
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        request.Content = content;
        var responseObject = await _httpClient.SendAsync(request);
        return responseObject;
    }
}