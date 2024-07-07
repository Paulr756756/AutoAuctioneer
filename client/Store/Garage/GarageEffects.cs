using Blazored.LocalStorage;
using Client.Models.Entities;
using Client.Shared;
using Fluxor;

namespace Client.Store.Garage;

public class GarageEffects
{
    private readonly GenericMethods _genericMethods;
    private readonly ILocalStorageService _localStorage;

    public GarageEffects(GenericMethods genericMethods, ILocalStorageService localStorage)
    {
        _genericMethods = genericMethods;
        _localStorage = localStorage;
    }

    [EffectMethod]
    public async Task HandleFetchDataAction(FetchGarageDataAction action, IDispatcher dispatcher)
    {
        var userId = (await _localStorage.GetItemAsStringAsync("userId")).Trim('"').Trim();
        List<ItemEntity>? items = new();
        List<CarEntity>? cars = new();
        List<PartEntity>? parts = new();
        List<ListingEntity>? listings = new();
        if (userId is not (null and "null"))
            try
            {
                items = await _genericMethods.GetValuesFromApi<ItemEntity>(
                    $"{EnvUrls.Item}{EnvUrls.GetOwned}?id={userId}");
                cars = await _genericMethods.GetValuesFromApi<CarEntity>(
                    $"{EnvUrls.Car}{EnvUrls.GetOwned}?id={userId}");
                parts = await _genericMethods.GetValuesFromApi<PartEntity>(
                    $"{EnvUrls.Part}{EnvUrls.GetOwned}?id={userId}");
                //TODO()
                listings = await _genericMethods.GetValuesFromApi<ListingEntity>(
                    $"{EnvUrls.Listing}{EnvUrls.GetOwned}?id={userId}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        dispatcher.Dispatch(new FetchGarageDataResultAction(items, cars, parts, listings));
    }

    [EffectMethod]
    public async Task HandleDeleteItemAction(DeleteGarageItemAction action, IDispatcher dispatcher)
    {
        var item = action.Item;
        HttpResponseMessage? response = null;
        if (item!.Type == 0)
            try
            {
                response = await _genericMethods.DeleteValuesFromApi(EnvUrls.Car + EnvUrls.Delete,
                    new { item.UserId, item.Id });
                if (!response.IsSuccessStatusCode) Console.WriteLine(response.ReasonPhrase!);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        else
            try
            {
                response = await _genericMethods.DeleteValuesFromApi(EnvUrls.Part + EnvUrls.Delete,
                    new { item.UserId, item.Id });
                if (!response.IsSuccessStatusCode) Console.WriteLine(response.ReasonPhrase!);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        dispatcher.Dispatch(new DeleteGarageItemResultAction(item, response));
    }
}