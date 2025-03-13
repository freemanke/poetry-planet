using Microsoft.AspNetCore.Components;
using Radzen;

namespace PoetryPlanet.Web;

public static class Extensions
{
    public static void ShowTooltip(this TooltipService TooltipService, ElementReference elementReference,
        string tooltip) =>
        TooltipService.Open(elementReference, tooltip,
            new TooltipOptions
            {
                Delay = 300,
                Position = TooltipPosition.Left
            });
}