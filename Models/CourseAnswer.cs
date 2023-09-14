using System;
using System.Collections.Generic;

namespace LearningManagementSystem.Models
{
    public partial class CourseAnswer
    {
        public int Id { get; set; }
        public string? Answers { get; set; }
        public int? CourseQuestionFk { get; set; }

        public virtual CourseQuestion? CourseQuestionFkNavigation { get; set; }
    }
}
