using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using IssueTracker.Data.Contracts;
using IssueTracker.Entities;
using IssueTracker.Repository.Contracts;

namespace IssueTracker.Repository
{
    public class IssueRepository : RepositoryBase<Issue>, IIssueRepository
    {
        public IssueRepository(IContext context)
            : base(context)
        {
        }

        public IEnumerable<Issue> GetByStatus(IssueStatus issueStatus)
        {
            var issues = _dbSet.Where(x => x.Status == issueStatus);
            return issues;
        }
    }
}
