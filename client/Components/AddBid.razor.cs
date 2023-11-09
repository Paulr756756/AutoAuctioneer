using Client.Models.Entities;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace Client.Components;

partial class AddBid
{
    private string BidAmtString { get; set; }
    private long BidAmount { get; set; }
    private List<BidEntity>? Bids { get; set; }
    [Parameter] public Guid ListingId { get; set; }
    protected override async Task OnInitializedAsync() {
        await base.OnInitializedAsync();
        await GetBids(ListingId);
    }
    async Task OnSubmit() {
        //TODO()
        try {
            BidAmount = long.Parse(BidAmtString);
            var response = await genericMethods.PostValuesToApi("", new {
                ListingId = ListingId,
                Userid = (await localStorage.GetItemAsStringAsync("userId")).Trim('"').Trim(),
                BidAmount = BidAmount,
            });
        } catch (Exception ex) {
            Console.WriteLine(ex.Message);
        }
    }
    async Task Cancel() {
        //TODO()

    }
    async Task GetBids(Guid listingId) {
        var userId = await localStorage.GetItemAsStringAsync("userId");
        var response = await genericMethods.PostValuesToApi("", new { UserId = userId, ListingId = listingId });
        List<BidEntity>? bids = null;
        if (response.IsSuccessStatusCode) bids = JsonSerializer.Deserialize<List<BidEntity>?>(response.Content.ReadAsStream());
        else Console.WriteLine(response.IsSuccessStatusCode.ToString());
        Bids = bids;
        Bids?.Sort((x, y) => y.BidAmount.CompareTo(x.BidAmount));
        BidAmount = Bids!.Count == 0 ? 0 : Bids[0].BidAmount + 1; 
        BidAmtString = BidAmount.ToString();
    }
}
