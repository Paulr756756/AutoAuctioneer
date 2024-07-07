using Microsoft.AspNetCore.Components;

namespace Client.Components.Tailwind;

partial class Card
{
    private string _badgeAttribute = "invisible";

    [Parameter] public RenderFragment CardImage { get; set; }
    [Parameter] public RenderFragment CardTitle { get; set; }
    [Parameter] public RenderFragment CardBody { get; set; }
    [Parameter] public RenderFragment CardActions { get; set; }
    [Parameter] public bool ContainsBadge { get; set; } = false;

    protected override void OnInitialized()
    {
        if (ContainsBadge) _badgeAttribute = "badge badge-secondary";
        base.OnInitialized();
    }
}