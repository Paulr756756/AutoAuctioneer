using System.Net.Http.Json;
using Client.Models.RequestModels;
using Client.Shared;
using Microsoft.AspNetCore.Components;

namespace Client.Components; 

partial class AddPart {
    private AddPartRequest _part { get; set; } = new();
    private string _marketPrice { get; set; }
    private string _partType { get; set; } = "0";
    private async Task OnFormSubmit() {
        ParseNumerics();
        var userId = (await _localStorageService.GetItemAsStringAsync("userId")).Trim('"').Trim();
        if (userId != null && userId != "null") {
            _part.UserId = Guid.Parse(userId);
            var response = await _httpClient.PostAsJsonAsync(EnvUrls.Part + EnvUrls.Add, _part);
            if (response.IsSuccessStatusCode) 
                _toastService.ShowSuccess("Added Car part successfully");
            else 
                _toastService.ShowError(response.ReasonPhrase!);
            
            Console.WriteLine(response.Content.ReadAsStringAsync());
        }else 
            _toastService.ShowError("Check yo id");
    }
    private void Cancel() {
        _part = new();
    }
    private void ParseNumerics() {
        _part.MarketPrice = _marketPrice==null?null:long.Parse(_marketPrice);
        _part.PartType = _partType==null?0:int.Parse(_partType);
    }

    private void PartTypeChanged() {
        _part.PartType = int.Parse(_partType);
        Console.WriteLine(_part.PartType);
        /*_part.PartType = ;*/
    }
}