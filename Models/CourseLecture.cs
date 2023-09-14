using System;
using System.Collections.Generic;

namespace LearningManagementSystem.Models
{
    public partial class CourseLecture
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? CourseFk { get; set; }
        public string? Video { get; set; }

        public virtual Course? CourseFkNavigation { get; set; }
    }
}
