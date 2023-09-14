using System;
using System.Collections.Generic;

namespace LearningManagementSystem.Models
{
    public partial class Batch
    {
        public Batch()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public int? LabFk { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public int? UsersFk { get; set; }

        public virtual Lab? LabFkNavigation { get; set; }
        public virtual User? UsersFkNavigation { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
