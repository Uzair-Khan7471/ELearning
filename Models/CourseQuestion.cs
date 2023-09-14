using System;
using System.Collections.Generic;

namespace LearningManagementSystem.Models
{
    public partial class CourseQuestion
    {
        public CourseQuestion()
        {
            CourseAnswers = new HashSet<CourseAnswer>();
        }

        public int Id { get; set; }
        public string? Questions { get; set; }
        public int? CourseFk { get; set; }

        public virtual Course? CourseFkNavigation { get; set; }
        public virtual ICollection<CourseAnswer> CourseAnswers { get; set; }
    }
}
