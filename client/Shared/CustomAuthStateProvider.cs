using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace auc_client.Shared; 

public class CustomAuthStateProvider : AuthenticationStateProvider {
    private readonly ILocalStorageService _localStorageService;
    private readonly HttpClient _httpClient;

    public CustomAuthStateProvider(ILocalStorageService localStorageService, HttpClient httpClient) {
        _localStorageService = localStorageService;
        _httpClient = httpClient;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync() {
        var token = await _localStorageService.GetItemAsStringAsync("token");
        var identity = new ClaimsIdentity();
        _httpClient.DefaultRequestHeaders.Authorization = null;

        if (!string.IsNullOrEmpty(token)) {
            identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));
        }

        var user = new ClaimsPrincipal(identity);
        
        //localstorage setting
        await _localStorageService.SetItemAsync("userName", user.Identity!.Name);
        await _localStorageService.SetItemAsync("userId",user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value);
        await _localStorageService.SetItemAsync("userRole", user.FindFirst(ClaimTypes.Role)?.Value);

        var state = new AuthenticationState(user);
        NotifyAuthenticationStateChanged(Task.FromResult(state));

        return state;
    }
    
    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }
}