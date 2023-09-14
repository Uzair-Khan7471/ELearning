using System;
using System.Collections.Generic;

namespace LearningManagementSystem.Models
{
    public partial class Status
    {
        public Status()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string? Status1 { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
