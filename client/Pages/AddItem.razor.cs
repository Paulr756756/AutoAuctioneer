using Client.Models.RequestModels;
using Client.Store.AddItem;
using Fluxor;
using Microsoft.AspNetCore.Components;

namespace Client.Pages;

partial class AddItem
{
    private AddCarRequest _car = new();

    private Type? _selectedType;
    private DynamicComponent? dc;
    [Inject] private IState<AddItemPageState> State { get; set; }
    [Inject] private IDispatcher Dispatcher { get; set; }

    private void OnDropDownChange(ChangeEventArgs e)
    {
        _selectedType = e.Value?.ToString()?.Length > 0 ? Type.GetType($"auc_client.Components.{e.Value}") : null;
        Dispatcher.Dispatch(new ItemTypeSelectAction(_selectedType));
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }
}