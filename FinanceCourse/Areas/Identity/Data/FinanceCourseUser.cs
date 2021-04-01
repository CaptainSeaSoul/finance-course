using FinanceCourse.Areas.Tools.Models;
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
        public virtual ICollection<CoursePageModel> CompletedPages { get; set; }
        [PersonalData]
        public virtual ICollection<ToolModel> ToolsStates { get; set; }
        [PersonalData]
        public virtual PersonalityTypeEnum? PersonalityType { get; set; }
    }
}
