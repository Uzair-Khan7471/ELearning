using System;
using System.Collections.Generic;

namespace LearningManagementSystem.Models
{
    public partial class Course
    {
        public Course()
        {
            CourseLectures = new HashSet<CourseLecture>();
            CourseQuestions = new HashSet<CourseQuestion>();
        }

        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public int? CategoryFk { get; set; }
        public int? Price { get; set; }
        public int? UsersFk { get; set; }

        public virtual Category? CategoryFkNavigation { get; set; }
        public virtual User? UsersFkNavigation { get; set; }
        public virtual ICollection<CourseLecture> CourseLectures { get; set; }
        public virtual ICollection<CourseQuestion> CourseQuestions { get; set; }
    }
}
