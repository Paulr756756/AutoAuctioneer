using Client.Models.Entities;
using Fluxor;

namespace Client.Store.Garage;

public static class GarageReducer
{
    [ReducerMethod]
    public static GarageState ReduceFetchGarageDataResultAction(GarageState state, FetchGarageDataResultAction action)
    {
        var items = action.Items;
        var cars = action.Cars;
        var parts = action.Parts;
        var listings = action.Listings;
        Dictionary<ItemEntity, CarEntity> CarMap = new();
        Dictionary<ItemEntity, PartEntity> PartMap = new();
        Dictionary<ItemEntity, ListingEntity> ListingMap = new();
        foreach (var item in items!)
        {
            if (item.Type == 0)
            {
                var car = cars?.FirstOrDefault(c => c.Id == item.Id);
                CarMap.Add(item, car!);
            }
            else
            {
                var part = parts?.FirstOrDefault(c => c.Id == item.Id);
                PartMap.Add(item, part!);
            }

            var listing = listings?.FirstOrDefault(l => l.ItemId == item.Id);
            //BugFixed()
            if (listing != null) ListingMap.Add(item, listing);
        }

        return new GarageState(false, items, CarMap, PartMap, ListingMap);
    }

    [ReducerMethod]
    public static GarageState ReduceDeleteGarageItemResultAction(GarageState state, DeleteGarageItemResultAction action)
    {
        var item = action.Item;
        var response = action.Response;
        if (!response!.IsSuccessStatusCode) return state;

        var carMap = state.CarsMap;
        var partsMap = state.PartsMap;
        var items = state.Items;
        var listingsMap = state.ListingsMap;
        items!.Remove(item!);
        carMap!.Remove(item!);
        partsMap!.Remove(item!);
        listingsMap!.Remove(item!);

        return new GarageState(false, items, carMap, partsMap, listingsMap);
    }
}