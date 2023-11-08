using auc_client.Models.DTOs;
using auc_client.Models.Entities;
using auc_client.Shared;
using Blazored.LocalStorage;
using Blazored.Toast.Services;

namespace auc_client.ViewModels;

public interface IGarageViewModel {
    List<CarEntity>? Cars { get; set; }
    List<PartEntity>? Parts { get; set; }
    List<ItemEntity>? Items { get; set; }
    Dictionary<ItemEntity, CarEntity> CarMapping { get; set; }
    Dictionary<ItemEntity, PartEntity> PartMapping { get; set; }
    Task Initialize();
    Task DeleteItem(ItemEntity item);
    event Action? StateChanged;
    void TriggerStateChange();
}

public class GarageViewModel : IGarageViewModel { 
    private readonly ILocalStorageService _localStorage;
    private readonly GenericMethods _genericMethods;
    private readonly IToastService _toastService;
    public List<CarEntity>? Cars { get; set; } = new();
    public List<PartEntity>? Parts { get; set; } = new();
    public List<ItemEntity>? Items { get; set; } = new();
    public List<ItemDTO> ItemDTOs { get; set; } = new();
    public List<CarDTO> CarDTOs { get; set; } = new();
    public List<PartDTO> PartDTOs { get; set; } = new();

    public Dictionary<ItemEntity, CarEntity> CarMapping { get; set; } = new();
    public Dictionary<ItemEntity, PartEntity> PartMapping { get; set; } = new();

    public GarageViewModel(ILocalStorageService localStorage, GenericMethods genericMethods, IToastService toastService) {
        _localStorage = localStorage;
        _genericMethods = genericMethods;
        _toastService = toastService;
    }
    //TODO(Test)
    public async Task Initialize() {
        /*if (Items.Count == 0) {
            var userId = (await _localStorage.GetItemAsStringAsync("userId")).Trim('"').Trim();
            if (userId is not (null and "null")) {
                Items = await _genericMethods.GetValuesFromApi<Item>($"{EnvUrls.Item}{EnvUrls.GetOwned}?id={userId}");
                Cars = await _genericMethods.GetValuesFromApi<Car>($"{EnvUrls.Car + EnvUrls.GetOwned}?id={userId}");
                Parts = await _genericMethods.GetValuesFromApi<Part>($"{EnvUrls.Part + EnvUrls.GetOwned}?id={userId}");
                foreach (var item in Items) {
                    if(item.Type==0) CarMapping.Add(item, Cars.FirstOrDefault(e=>e.Id==item.Id)!);
                    else PartMapping.Add(item, Parts.FirstOrDefault(e=>e.Id == item.Id)!);
                }
                TriggerStateChange();
            }*/
        var userId = (await _localStorage.GetItemAsStringAsync("userId")).Trim('"').Trim();
        if (userId is not (null and "null")) {
            Items = await _genericMethods.GetValuesFromApi<ItemEntity>($"{EnvUrls.Item}{EnvUrls.GetOwned}?id={userId}");
            Cars = (await _genericMethods.GetValuesFromApi<CarEntity>($"{EnvUrls.Car}{EnvUrls.GetOwned}?id={userId}"));
            Parts = await _genericMethods.GetValuesFromApi<PartEntity>($"{EnvUrls.Part}{EnvUrls.GetOwned}?id={userId}");
            foreach (var item in Items!) {
                ItemDTOs.Add((ItemDTO)item!);
                if (item.Type == 0) {
                    var car = Cars?.FirstOrDefault(c => c.Id == item.Id);
                    CarMapping.Add(item, car!);
                    /*CarDTOs.Add((CarDTO)car!);*/
                } else {
                    var part = Parts?.FirstOrDefault(c => c.Id == item.Id);
                    PartMapping.Add(item, part!);
                    /*PartDTOs.Add((PartDTO)part!);*/
                }
            }
            TriggerStateChange();
        }
    }
    //TODO(Test)
    public async Task DeleteItem(ItemEntity item) {
        if (item.Type == 0) {
            // var response = await _genericMethods.GetValuesFromApi<Item>($"{EnvUrls.Item}");
            try {
                var httpResponse = await _genericMethods.DeleteValuesFromApi(EnvUrls.Car + EnvUrls.Delete, 
                    new { UserId = item.UserId, Id = item.Id});
                if (httpResponse.IsSuccessStatusCode) {
                    Cars!.Remove(CarMapping[item]);
                    CarMapping.Remove(item);
                    Items!.Remove(item);
                    _toastService.ShowSuccess("Car deleted");
                } else _toastService.ShowError(httpResponse.ReasonPhrase!);
            } catch (Exception ex) {
                _toastService.ShowError(ex.Message);
            }
        }
        else {
            try {
                var httpResponse = await _genericMethods.DeleteValuesFromApi(EnvUrls.Part+EnvUrls.Delete,
                    new { UserId = item.UserId, Id = item.Id});
                if (httpResponse.IsSuccessStatusCode) {
                    Parts!.Remove(PartMapping[item]);
                    PartMapping.Remove(item);
                    Items!.Remove(item);
                    _toastService.ShowSuccess("Part deleted");
                } else _toastService.ShowError(httpResponse.ReasonPhrase!);

            } catch (Exception ex) {
                _toastService.ShowError(ex.Message);
            }
            
        }
        TriggerStateChange();
    }
    public event Action? StateChanged;
    public void TriggerStateChange() {
        StateChanged?.Invoke();
    }

}