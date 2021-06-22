using FinanceCourse.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceCourse.Data
{
    public class CoursePageModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public virtual CourseModel ParentCourse { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public int? TaskTool { get; set; }
        public ICollection<FinanceCourseUser> PassedUsers { get; set; }
        public ICollection<PersonalAdvice> PersonalAdvice { get; set; }
    }
}
