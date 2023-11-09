using Client.Models.Entities;
using Client.Shared;
using Client.Store.Garage;
using Blazored.Toast.Services;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Client.Pages; 

partial class Garage {
    [Inject] private IState<GarageState> State { get; set; }
    [Inject] private IDispatcher Dispatcher { get; set; }
    private string _optionSelected { get; set; } = "Filter";

    protected override async Task OnInitializedAsync() {
        await base.OnInitializedAsync();
        var action = new FetchGarageDataAction();
        Dispatcher.Dispatch(action);
    }

    void SelectChanged() {
        
    }
    
    void ViewDetails() {
        
    }
    private void DeleteItem(ItemEntity? item) {
        var action = new DeleteGarageItemAction(item);
        Dispatcher.Dispatch(action);
    }
    private async Task ListItem(ItemEntity item) {
        var response = await GenericMethods.PostValuesToApi($"{EnvUrls.Listing}{EnvUrls.Add}", item);
        if (!response.IsSuccessStatusCode) {
            Dispatcher.Dispatch(new FetchGarageDataAction());
            ToastService.ShowSuccess("Item Listed");
        } else ToastService.ShowError("Couldn't Add to your listings");
    }
    public async Task ShowListModal(string ElementId) {
        Console.WriteLine("List Modal Clicked");
        await JsRuntime.InvokeVoidAsync("eval", $"document.getElementById('{ElementId}').showModal();");
    }
    public async Task CloseListModal(string ElementId) {
        await JsRuntime.InvokeVoidAsync("eval", $"document.getElementById('{ElementId}').close();");
    }
    public async Task ShowDeleteModal(Guid ElementId) {
        Console.WriteLine("Modal Clicked");
        await JsRuntime.InvokeVoidAsync("eval", $"document.getElementById('{ElementId}').showModal();");
    }
    public async Task CloseDeleteModal(Guid ElementId) {
        await JsRuntime.InvokeVoidAsync("eval", $"document.getElementById('{ElementId}').close();");
    }    
}