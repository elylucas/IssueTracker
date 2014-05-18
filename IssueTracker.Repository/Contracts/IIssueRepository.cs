using System.Collections;
using System.Collections.Generic;
using IssueTracker.Entities;

namespace IssueTracker.Repository.Contracts
{
    public interface IIssueRepository : IRepository<Issue>
    {
        IEnumerable<Issue> GetByStatus(IssueStatus issueStatus);
    }
}
