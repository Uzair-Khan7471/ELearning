using System;
using System.Collections.Generic;

namespace LearningManagementSystem.Models
{
    public partial class User
    {
        public User()
        {
            Batches = new HashSet<Batch>();
            Courses = new HashSet<Course>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public long? Phone { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public int? Gender { get; set; }
        public int? StatusFk { get; set; }
        public int? BatchFk { get; set; }
        public int? RoleFk { get; set; }
        public string? Image { get; set; }

        public virtual Batch? BatchFkNavigation { get; set; }
        public virtual Gender? GenderNavigation { get; set; }
        public virtual Role? RoleFkNavigation { get; set; }
        public virtual Status? StatusFkNavigation { get; set; }
        public virtual ICollection<Batch> Batches { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
