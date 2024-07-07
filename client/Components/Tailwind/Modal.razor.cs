using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Client.Components.Tailwind;

partial class Modal
{
    [Parameter] public string ButtonColor { get; set; }
    [Parameter] public string ButtonText { get; set; }
    [Parameter] public RenderFragment ChildContent { get; set; }
    [Parameter] public string ElementId { get; set; }

    public async Task ShowModal()
    {
        await JsRuntime.InvokeVoidAsync("eval", $"document.getElementById('{ElementId}').showModal();");
    }

    public async Task CloseModal()
    {
        await JsRuntime.InvokeVoidAsync("eval", $"document.getElementById('{ElementId}').close();");
    }
}