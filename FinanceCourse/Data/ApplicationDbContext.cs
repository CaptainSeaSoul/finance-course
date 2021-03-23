using FinanceCourse.Areas.Identity.Data;
using FinanceCourse.Areas.Tools.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceCourse.Data
{
    public class ApplicationDbContext : IdentityDbContext<FinanceCourseUser>
    {
        public DbSet<CourseModel> Courses { get; set; }
        public DbSet<CoursePageModel> CoursePages { get; set; }
        public DbSet<ToolModel> Tools { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ToolModel>()
               .Property<string>("ToolDataStr")
               .HasField("_toolData");
        }
    }
}
