using System;
using System.Collections.Generic;

namespace LearningManagementSystem.Models
{
    public partial class Gender
    {
        public Gender()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string? Gender1 { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
