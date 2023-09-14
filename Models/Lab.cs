using System;
using System.Collections.Generic;

namespace LearningManagementSystem.Models
{
    public partial class Lab
    {
        public Lab()
        {
            Batches = new HashSet<Batch>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Batch> Batches { get; set; }
    }
}
