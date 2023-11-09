using Client.Models;
using Client.Models.Entities;
using Client.Shared;
using Client.Store.Base;
using Client.Store.Garage;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Client.Pages;

partial class ListedItems {
    private List<ListingResponse>? Listings { get; set; } = new();
    [Inject] private IState<GarageState> GarageState { get; set;}
    [Inject] private IState<BaseState> BaseState { get; set; }
    private  Guid UserId { get; set; }

    protected override async Task OnInitializedAsync()
    {
        //TODO(Fix this)
        // BaseState.Value.JwtToken != null;
        var listingsResponse = await GetResponse();
        if(listingsResponse != null) { 
            Listings = listingsResponse; 
        }
    }
    async Task<List<ListingResponse>?> GetResponse() 
    {
        List<ListingResponse>? response = await _genericMethods.GetValuesFromApi<ListingResponse>(EnvUrls.GetListings);
        //Remove all listings by the logged in user.
        if(!response.IsNullOrEmpty() )  response?.RemoveAll(r=>r.UserId == UserId);
        return response;
    }

/*    async Task<CarEntity?> FetchCar(Guid id) {
        var car = await _genericMethods.GetValuesFromApi<CarEntity>($"{EnvUrls.Car + EnvUrls.GetById}?id={id}");
        return car?.FirstOrDefault();
    }
    async Task<PartEntity?> FetchPart(Guid id) {
        var part = await _genericMethods.GetValuesFromApi<PartEntity>($"{EnvUrls.Part + EnvUrls.GetById}?id={id}")  ;
        return part?.FirstOrDefault();
    }
    async Task<(BidEntity,BidEntity)> FetchBid(Guid id) {
        //TODO(First fetch the highest from select* where itemid=; Then fetch higest from select * where itemid= and userid=)
        BidEntity highestBid = null;
        BidEntity yourHighestBid = null;
        return (highestBid,yourHighestBid);
    }
*/
}