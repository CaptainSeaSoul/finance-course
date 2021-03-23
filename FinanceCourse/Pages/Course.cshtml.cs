using FinanceCourse.Areas.Identity.Data;
using FinanceCourse.Areas.Tools.Services;
using FinanceCourse.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace FinanceCourse.Pages
{
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

        public void OnGet(int id)
        {
            Id = id;

            Course = _dbContext.Courses.Single(c => c.Id == 1);
            _dbContext.Entry(Course)
                .Collection(c => c.Pages)
                .Load();
        }
    }
}
