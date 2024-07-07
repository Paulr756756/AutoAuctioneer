using Blazored.LocalStorage;
using Blazored.Toast.Services;
using Client.Models.RequestModels;
using Client.Shared;
using Microsoft.AspNetCore.Components;

namespace Client.ViewModels;

public interface IAddItemViewModel
{
    public Type? SelectedType { get; set; }
    public DynamicComponent? Dc { get; set; }
    Task Initialize();
    void OnDropDownChange(ChangeEventArgs eventArgs);
}

public class AddItemViewModel : IAddItemViewModel
{
    private readonly GenericMethods _genericMethods;
    private readonly ILocalStorageService _localStorage;
    private readonly IToastService _toastService;

    public AddItemViewModel(ILocalStorageService localStorage, GenericMethods genericMethods,
        IToastService toastService)
    {
        _localStorage = localStorage;
        _genericMethods = genericMethods;
        _toastService = toastService;
    }

    //For Car Request
    public AddCarRequest CarRequest { get; set; }
    public string HorsePower { get; set; }
    public string Torque { get; set; }
    public string FuelEfficiency { get; set; }
    public string Acceleration { get; set; }
    public string TopSpeed { get; set; }

    public string MainImageUrl { get; set; }

    //For Part Request
    public AddPartRequest PartRequest { get; set; }

    public Type? SelectedType { get; set; }
    public DynamicComponent? Dc { get; set; }

    public async Task Initialize()
    {
    }

    public void OnDropDownChange(ChangeEventArgs eventArgs)
    {
        SelectedType = eventArgs.Value?.ToString()?.Length > 0
            ? Type.GetType($"auc_client.Components.{eventArgs.Value}")
            : null;
    }
}