using Client.Models.Entities;

namespace Client.Store.Garage;

public class FetchGarageDataResultAction {
    public List<ItemEntity>? Items { get; }
    public List<CarEntity>? Cars { get; }
    public List<PartEntity>? Parts { get;}
    public List<ListingEntity>? Listings { get; }

    public FetchGarageDataResultAction(List<ItemEntity>? items, List<CarEntity>? cars, List<PartEntity>? parts, List<ListingEntity>? listings) {
        Items = items;
        Cars = cars;
        Parts = parts;
        Listings = listings;
    }
}