using System.Net.Http.Json;
using auc_client.Models.RequestModels;
using auc_client.Shared;

namespace auc_client.Components; 

partial class AddCar {
    private AddCarRequest CarRequest { get; set; } = new();
    private string Horsepower { get; set; }
    private string Torque { get; set; }
    private string FuelEfficiency { get; set; }
    private string Acceleration { get; set; }
    private string TopSpeed { get; set; }
    private string MainImage { get; set; }
    
    async Task OnFormSubmit() {
        ParseNumerics();
        /*using StringContent jsonContent = new(JsonSerializer.Serialize(_car), Encoding.UTF8, "application/json")*/;
        var userid = (await _localStorageService.GetItemAsStringAsync("userId")).Trim('"').Trim();
        if (userid != null && userid != "null") {
            CarRequest.UserId = Guid.Parse(userid);
            var responseMessage = await _httpClient.PostAsJsonAsync(new Uri($"{_httpClient.BaseAddress}{EnvUrls.Car}{EnvUrls.StoreCar}"), CarRequest);
            if (responseMessage.IsSuccessStatusCode) 
                _toastService.ShowSuccess("Car was added successfully to your garage");
            else _toastService.ShowError("Could not add it to your garage");
        
            Console.WriteLine(responseMessage.Content.ReadAsStringAsync());
        }else _toastService.ShowError("Check yo id");
        
    }

    void Cancel() {
        CarRequest = new();
    }
    void ParseNumerics() {
        CarRequest.Horsepower = int.Parse(Horsepower);
        CarRequest.Acceleration = double.Parse(Acceleration);
        CarRequest.Torque = int.Parse(Torque);
        CarRequest.TopSpeed = int.Parse(TopSpeed);
        CarRequest.FuelEfficiency = int.Parse(FuelEfficiency);
        CarRequest.ImageUrls.Add(MainImage);
    }
}