using FinanceCourse.Areas.Tools.Models;
using FinanceCourse.Areas.Tools.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceCourse.Areas.Tools.Components
{
    public partial class PaperTradingComponent : BlazorToolBase
    {
        [Inject]
        private TradingService _tradingService { get; set; }
        [Inject]
        private IJSRuntime JS { get; set; }

        private PaperTradingModel model = null;

        protected override void OnInitialized()
        {
            if (model == null)
                model = new(_tradingService);
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            if (User.ToolsStates.Where(ts => ts.ToolId == 5).Any()) 
                model = new PaperTradingModel(User.ToolsStates.First(ts => ts.ToolId == 5), _tradingService);

            await model.CalculateCurrentPossesionAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            var data = await model.CalculateNetWorthGraphAsync();

            if (firstRender)
                await JS.InvokeVoidAsync("drawNetWorthGraph", data.ToArray());
            else
                await JS.InvokeVoidAsync("updateNetWorthGraphData", data.ToArray());
        }

        private void StockAmountChanged(string symbol, int updAmount)
        {
            int change = updAmount - model.CurrentPossesion[symbol].Amount;

            model.AddTransaction(model.CurrentPossesion[symbol].Price, symbol, change);
        }

        private async void ApplyChangesAsync()
        {
            await SaveStateAsync(model);
        }

    }
}
