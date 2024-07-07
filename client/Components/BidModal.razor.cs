using Client.Components.Tailwind;
using Client.Models;
using Microsoft.AspNetCore.Components;

namespace Client.Components;

partial class BidModal
{
    private Modal _modalRef;
    private string _bidamt { get; set; } = "0";
    [Parameter] public string ElementId { get; set; }
    private AddBidRequest BidRequest { get; set; } = new();

    private async Task Add()
    {
        BidRequest.BidAmount = long.Parse(_bidamt);
        _modalRef.CloseModal();
    }

    private async Task Cancel()
    {
        BidRequest = new AddBidRequest();
        _bidamt = "0";
        _modalRef.CloseModal();
    }
}