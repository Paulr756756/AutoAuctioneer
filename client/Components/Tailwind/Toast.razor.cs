using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace auc_client.Components.Tailwind; 

partial class Toast {
    [Parameter] public RenderFragment ChildContent { get; set; } = default!;
    [Parameter] public string Type { get; set; } = "info";
    [Parameter] public string ClassAttributes { get; set; } = "";
    [Parameter] public string ToastId { get; set; } = "default_toast";

    protected override Task OnInitializedAsync() {
        return base.OnInitializedAsync();
    }

    public async Task PopUp() {
        await _jsRuntime.InvokeVoidAsync("eval", $"document.getElementById('{ToastId}').classList.add('show')");
    }

    public async Task ShowToast() {
        
    }

    public async Task CloseToast() {
        
    }
    
}