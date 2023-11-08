using auc_client.Components.Tailwind;
using auc_client.Models;
using Microsoft.AspNetCore.Components;

namespace auc_client.Components; 

partial class BidModal {
    private Modal _modalRef;
    private string _bidamt { get; set; } = "0";
    [Parameter] public string ElementId { get; set; } 
    private AddBidRequest BidRequest { get; set; } = new();

    async Task Add() {
        BidRequest.BidAmount = long.Parse(_bidamt);
        _modalRef.CloseModal();
    }

    async Task Cancel() {
        BidRequest = new();
        _bidamt = "0";
        _modalRef.CloseModal();
    }
}