using FinanceCourse.Areas.Identity.Data;
using FinanceCourse.Areas.Tools.Models;
using FinanceCourse.Areas.Tools.Services;
using FinanceCourse.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FinanceCourse.Areas.Tools.Components
{
    public class BlazorToolBase : ComponentBase
    {
        [Inject]
        protected ApplicationDbContext _db { get; set; }
        [Inject]
        protected AuthenticationStateProvider _authenticationStateProvider { get; set; }
        protected FinanceCourseUser User = null;

        protected override async Task OnInitializedAsync()
        {
            await GetCurrentUserAsync();
        }

        protected async Task SaveStateAsync(ToolModel updModel)
        {
            // get current user
            if (User == null)
                await GetCurrentUserAsync();

            updModel.SaveJson();

            // update user's tool state
            var seq = User.ToolsStates.Where(m => m.ToolId == updModel.ToolId);
            if (seq.Any())
                seq.First().ToolDataJson = updModel.ToolDataJson; 
            else
                User.ToolsStates.Add(updModel); 
            
            _db.Update(User);
            await _db.SaveChangesAsync();
        }

        protected async Task GetCurrentUserAsync()
        {
            AuthenticationState authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            string currentUserId = authState.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            User = await _db.Users
                .Include(u => u.ToolsStates)
                .FirstOrDefaultAsync(u => u.Id == currentUserId);
        }
    }
}
