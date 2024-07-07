using Client.Models.Entities;
using Client.Store.Garage;
using Fluxor;
using Microsoft.AspNetCore.Components;

namespace Client.Pages;

partial class ItemDetails
{
    private readonly CarEntity? _car;
    private readonly ItemEntity? _item;
    private readonly PartEntity? _part;
    private readonly ListingEntity? listing;
    [Inject] private IState<GarageState> GarageState { get; set; }

    [Parameter] public string? StringIdParam { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        /* TODO(Try to get value directly from the application state instead of calling it again and again)
         * GarageState.Value.ListingsMap?.TryGetValue(StringIdParam, listing);*/
    }
}