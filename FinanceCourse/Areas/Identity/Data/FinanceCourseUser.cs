using FinanceCourse.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceCourse.Areas.Identity.Data
{
    public class FinanceCourseUser : IdentityUser
    {
        [PersonalData]
        public virtual ICollection<CoursePage> CompletedPages { get; set; }
    }
}
