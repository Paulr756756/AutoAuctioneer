using Client.Models.Entities;
using Fluxor;

namespace Client.Store.Garage;

[FeatureState]
public class GarageState
{
    //Required by fluxor for creating initial state
    private GarageState()
    {
    }

    public GarageState(
        bool isLoading,
        List<ItemEntity>? items, Dictionary<ItemEntity, CarEntity>? carsMap,
        Dictionary<ItemEntity, PartEntity>? partsMap, Dictionary<ItemEntity, ListingEntity>? listingsMap)
    {
        IsLoading = isLoading;
        Items = items;
        CarsMap = carsMap;
        PartsMap = partsMap;
        ListingsMap = listingsMap;
    }

    public bool IsLoading { get; }

    /*    public List<CarEntity>? Cars { get; }
        public List<PartEntity>? Parts { get; }*/
    public List<ItemEntity>? Items { get; } = new();
    public Dictionary<ItemEntity, CarEntity>? CarsMap { get; } = new();
    public Dictionary<ItemEntity, PartEntity>? PartsMap { get; } = new();
    public Dictionary<ItemEntity, ListingEntity>? ListingsMap { get; } = new();
}