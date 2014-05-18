using System;
using System.ComponentModel.DataAnnotations;

namespace IssueTracker.Entities
{
    public class Issue : EntityBase
    {
        [Required]
        public string Description { get; set; }
        public Person Assignee { get; set; }
        public int? AssigneeId { get; set; }
        public IssueStatus Status { get; set; }
        public DateTime? StartedDateTime { get; set; }
        public DateTime? CompletedDateTime { get; set; }

        public void Start()
        {
            this.StartedDateTime = DateTime.UtcNow;
            this.Status = IssueStatus.Started;
        }

        public void Finish()
        {
            this.CompletedDateTime = DateTime.UtcNow;
            this.Status = IssueStatus.Finished;
        }
    }

    public enum IssueStatus
    {
        NotStarted = 0,
        Started = 100,
        Finished = 200
    }


}
