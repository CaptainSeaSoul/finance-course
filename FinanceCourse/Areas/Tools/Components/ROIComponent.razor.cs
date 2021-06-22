using FinanceCourse.Areas.Tools.Models;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static FinanceCourse.Areas.Tools.Models.ROIModel;

namespace FinanceCourse.Areas.Tools.Components
{
    public partial class ROIComponent : BlazorToolBase
    {
        private ROIModel componentModel = new();
        private FormPackage additionalRow = new();
        private EditContext editContext;
        private EditContext additionalEditContext;

        protected override void OnInitialized()
        {
            editContext = new EditContext(componentModel);
            additionalEditContext = new EditContext(additionalRow);
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            if (User.ToolsStates.Where(ts => ts.ToolId == 3).Any())
            {
                componentModel = new(User.ToolsStates.First(ts => ts.ToolId == 3));
                editContext = new EditContext(componentModel);
            }
        }

        private async void HandleValidSubmitAsync()
        {
            await SaveStateAsync(componentModel);
        }

        private void AddRow()
        {
            if (additionalEditContext.Validate())
            {
                componentModel.FormModel.Add(new(additionalRow));

                additionalRow.Reset();
            }
        }

        private void RemoveRow(int rowId)
        {
            componentModel.FormModel.RemoveAt(rowId);
        }

        private async void SendROIAsync()
        {
            string table = "<table border=\"1\" style=\"border-collapse: collapse;\">" +
                "<thead>" +
                "<tr>" +
                    "<th scope=\"col\">Name</th>" +
                    "<th scope=\"col\">Cost</th>" +
                    "<th scope=\"col\">Time Cost in Hours</th>" +
                    "<th scope=\"col\">Difficulty (0 to 10)</th>" +
                    "<th scope=\"col\">Estimated Return</th>" +
                    "<th scope=\"col\">Confidence (0 to 1)</th>" +
                    "<th scope=\"col\">Start Date</th>" +
                    "<th scope=\"col\">End Date</th>" +
                    "<th scope=\"col\">Intangibles</th>" +
                    "<th scope=\"col\">Return * Confidence</th>" +
                    "<th scope=\"col\">Actual Return</th>" +
                "</tr>" +
            "</thead><tbody>";

            foreach (var pack in componentModel.FormModel)
                table += "<tr>" +
                    $"<td>{pack.Name}</td>" +
                    $"<td>{pack.Cost}</td>" +
                    $"<td>{pack.HoursTimeCost}</td>" +
                    $"<td>{pack.Difficulty}</td>" +
                    $"<td>{pack.EstimatedReturn}</td>" +
                    $"<td>{pack.Confidence}</td>" +
                    $"<td>{pack.StartDate}</td>" +
                    $"<td>{pack.EndDate}</td>" +
                    $"<td>{pack.Intengibles}</td>" +
                    $"<td>{pack.ReturnConfidence}</td>" +
                    $"<td>{pack.RealReturn}</td></tr>";

            table += "</tbody></table>";

            await _mailService.SendEmailAsync(User.Email, "Day 4 ROI table", table);
        }
    }
}
