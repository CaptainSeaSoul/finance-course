using FinanceCourse.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace FinanceCourse.Data
{
    public class PersonalAdvice
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public CoursePageModel ParentPage { get; set; } 
        public PersonalityTypeEnum PersonalityType { get; set; }
        public string advice { get; set; }
    }
}