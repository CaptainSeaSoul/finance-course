using FinanceCourse.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceCourse.Data
{
    public class ApplicationDbContext : IdentityDbContext<FinanceCourseUser>
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<CoursePage> CoursePages { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
