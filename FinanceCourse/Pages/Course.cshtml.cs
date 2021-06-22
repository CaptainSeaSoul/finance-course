using FinanceCourse.Areas.Identity.Data;
using FinanceCourse.Areas.Tools.Services;
using FinanceCourse.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceCourse.Pages
{
    [Authorize]
    public class CourseModel : PageModel
    {
        UserManager<FinanceCourseUser> _userManager;
        ApplicationDbContext _dbContext;
        public ToolComponentService _ToolService { get; private set; }
        public int Id { get; private set; }
        public Data.CourseModel Course { get; private set; }

        public CourseModel(UserManager<FinanceCourseUser> manager, ApplicationDbContext context, ToolComponentService toolService)
        {
            _userManager = manager;
            _dbContext = context;
            _ToolService = toolService;
        }

        public async Task<string> GetPersonalAdviceAsync(int pageId)
        {
            var user = await _userManager.GetUserAsync(User);

            if (!user.PersonalityType.HasValue)
                return "Pass personality test to get the personal advice";
            
            var page = Course.Pages.Single(p => p.Id == pageId);

            await _dbContext.Entry(page)
                .Collection(p => p.PersonalAdvice)
                .LoadAsync();

            if (page.PersonalAdvice == null)
                return "";

            return page.PersonalAdvice
                .SingleOrDefault(pa => pa.PersonalityType == user.PersonalityType && pa.ParentPage.Id == pageId)?.advice;
        }

        public void OnGet(int id)
        {
            Id = id;

            Course = _dbContext.Courses.Single(c => c.Id == Id);
            _dbContext.Entry(Course)
                .Collection(c => c.Pages)
                .Load();
        }
    }
}
