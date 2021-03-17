using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceCourse.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinanceCourse.Pages
{
    public class CourseListModel : PageModel
    {
        public ApplicationDbContext _db { get; private set; }
        public IEnumerable<Course> Courses { get; private set; }
        public CourseListModel (ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            Courses = _db.Courses;
        }
    }
}
