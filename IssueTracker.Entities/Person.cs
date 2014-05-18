using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IssueTracker.Entities
{
    public class Person : EntityBase
    {
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        public ICollection<Issue> Issues { get; set; }
    }
}
