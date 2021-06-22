using FinanceCourse.Areas.Tools.Models;
using FinanceCourse.Areas.Tools.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using WebPortfolio.Services;

namespace FinanceCourse.Areas.Tools.Components
{
    public partial class PainterComponent : BlazorToolBase
    {
        [Inject]
        private IJSRuntime JS { get; set; }

        private SavePaintingInvokeHelper savePaintingInvokeHelper;

        private ToolModel toolModel { get; set; } = new() { ToolId = 1 };

        protected override void OnInitialized()
        {
            base.OnInitialized();

            savePaintingInvokeHelper = new SavePaintingInvokeHelper(SavePaintingAsync);
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            if (User.ToolsStates.Where(ts => ts.ToolId == 1).Any())
                toolModel = User.ToolsStates.First(ts => ts.ToolId == 1);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await JS.InvokeVoidAsync("LCinitOnVisible",
                DotNetObjectReference.Create(savePaintingInvokeHelper),
                toolModel.ToolDataJson);
        }

        private async Task SavePaintingAsync(string jsonPainting)
        {
            toolModel.ToolDataJson = jsonPainting;
            await SaveStateAsync(toolModel);
        }

        private async void SendPaintingAsync()
        {
            string filePath = await SaveTempPaintingAsync();

            await (_mailService as MailKitEmailService).SendEmailAsync(User.Email, "Day 2 dream drawing", "Your dream in the attachment!", new string[]{ filePath });
                        
            File.Delete(filePath);
        }

        private async Task<string> SaveTempPaintingAsync()
        {
            string imagestr = await JS.InvokeAsync<string>("LCgetData64Base");

            var path = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".png");

            byte[] bytes = Convert.FromBase64String(imagestr.Replace("data:image/png;base64,", ""));

            using (MemoryStream ms = new MemoryStream(bytes))
            {
                Image pic = Image.FromStream(ms);

                pic.Save(path);
            }

            return path;
        }
    }
}
