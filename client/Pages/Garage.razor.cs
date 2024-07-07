using Client.Models.Entities;
using Client.Shared;
using Client.Store.Garage;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Client.Pages;

partial class Garage
{
    [Inject] private IState<GarageState> State { get; set; }
    [Inject] private IDispatcher Dispatcher { get; set; }
    private string _optionSelected { get; set; } = "Filter";

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        var action = new FetchGarageDataAction();
        Dispatcher.Dispatch(action);
    }

    private void SelectChanged()
    {
    }

    private void ViewDetails()
    {
    }

    private void DeleteItem(ItemEntity? item)
    {
        var action = new DeleteGarageItemAction(item);
        Dispatcher.Dispatch(action);
    }

    private async Task ListItem(ItemEntity item)
    {
        var response = await GenericMethods.PostValuesToApi(EnvUrls.Listing.Concat(EnvUrls.Add).ToString()!, item);
        if (!response.IsSuccessStatusCode)
        {
            Dispatcher.Dispatch(new FetchGarageDataAction());
            ToastService.ShowSuccess("Item Listed");
        }
        else
        {
            ToastService.ShowError("Couldn't Add to your listings");
        }
    }

    private async Task RemoveFromListings()
    {
        //var response = await GenericMethods.PostValuesToApi(EnvUrls.Listing.Concat(EnvUrls.Delete).ToString(), );
    }

    private async Task AddToListModal(string ElementId)
    {
        Console.WriteLine("List Modal Clicked");
        await JsRuntime.InvokeVoidAsync("eval", $"document.getElementById('{ElementId}').showModal();");
    }

    private async Task ShowModal(string ElementId)
    {
        Console.WriteLine("Modal Opened");
        await JsRuntime.InvokeVoidAsync("eval", $"document.getElementById('{ElementId}').showModal();");
    }

    private async Task DeleteItemModal(Guid ElementId)
    {
        Console.WriteLine("Modal Clicked");
        await JsRuntime.InvokeVoidAsync("eval", $"document.getElementById('{ElementId}').showModal();");
    }

    public async Task CloseModal(string ElementId)
    {
        await JsRuntime.InvokeVoidAsync("eval", $"document.getElementById('{ElementId}').close();");
    }
}