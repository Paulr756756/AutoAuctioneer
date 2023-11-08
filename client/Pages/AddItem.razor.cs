using auc_client.Components;
using auc_client.Models.RequestModels;
using auc_client.Store.AddItem;
using Fluxor;
using Microsoft.AspNetCore.Components;

namespace auc_client.Pages;

partial class AddItem {
    [Inject] IState<AddItemPageState> State { get; set; }
    [Inject] IDispatcher Dispatcher { get; set; }

    private Type? _selectedType;
    private AddCarRequest _car = new();
    private DynamicComponent? dc;

    private void OnDropDownChange(ChangeEventArgs e) {
        _selectedType = e.Value?.ToString()?.Length > 0 ?
            Type.GetType($"auc_client.Components.{e.Value}") : null;
        Dispatcher.Dispatch(new ItemTypeSelectAction(_selectedType));
    }

    protected override async Task OnInitializedAsync() {
        await base.OnInitializedAsync();
    }
}