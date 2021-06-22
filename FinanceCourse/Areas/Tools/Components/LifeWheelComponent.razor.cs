using FinanceCourse.Areas.Tools.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebPortfolio.Services;
using static FinanceCourse.Areas.Tools.Models.LifeWheelModel;

namespace FinanceCourse.Areas.Tools.Components
{
    public partial class LifeWheelComponent : BlazorToolBase
    {
        [Inject]
        private IJSRuntime JS { get; set; }

        private LifeWheelModel lifeWheelModel = new();
        private NameScorePair additionalRow = new();
        private EditContext editContext;
        private EditContext additionalEditContext;

        protected override void OnInitialized()
        {
            editContext = new EditContext(lifeWheelModel);
            additionalEditContext = new EditContext(additionalRow);
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            if (User.ToolsStates.Where(ts => ts.ToolId == 2).Any())
            {
                lifeWheelModel = new LifeWheelModel(User.ToolsStates.First(ts => ts.ToolId == 2));
                editContext = new EditContext(lifeWheelModel);
            }

            editContext.OnFieldChanged += HandleFieldChangedAsync;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            List<string> labels = new();
            List<int> data = new();

            foreach (var pair in lifeWheelModel.LifeStats)
            {
                labels.Add(pair.Name);
                data.Add(pair.Score);
            }

            if (firstRender)
                await JS.InvokeVoidAsync("drawLifeWheelChart", labels.ToArray(), data.ToArray());
            else
                await JS.InvokeVoidAsync("updateLifeWheelData", labels.ToArray(), data.ToArray());
        }

        private async void HandleFieldChangedAsync(object sender, FieldChangedEventArgs e)
        {
            if (editContext.Validate())
                await SaveStateAsync(lifeWheelModel);

            StateHasChanged();
        }

        private async Task AddRowAsync()
        {
            if (additionalEditContext.Validate())
            {
                lifeWheelModel.LifeStats.Add(new (additionalRow));

                additionalRow.Reset();

                await SaveStateAsync(lifeWheelModel);
            }
        }

        private async Task RemoveRowAsync(int rowId)
        {
            lifeWheelModel.LifeStats.RemoveAt(rowId);

            await SaveStateAsync(lifeWheelModel);
        }

        private async void SendWheelAsync()
        {
            string filePath = await SaveTempPaintingAsync();

            await (_mailService as MailKitEmailService).SendEmailAsync(User.Email, "Day 3 life wheel picture", "Your life wheel in the attachment!", new string[] { filePath });

            File.Delete(filePath);
        }

        private async Task<string> SaveTempPaintingAsync()
        {
            string imagestr = await JS.InvokeAsync<string>("getLifeWheelDrawing64Base");

            var path = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".png");

            byte[] bytes = Convert.FromBase64String(imagestr.Replace("data:image/jpeg;base64,", ""));

            using (MemoryStream ms = new MemoryStream(bytes))
            {
                Image pic = Image.FromStream(ms);

                pic.Save(path);
            }

            return path;
        }
    }
}
