using IssueTracker.Data.Contracts;
using IssueTracker.Entities;
using IssueTracker.Repository.Contracts;

namespace IssueTracker.Repository
{
    public class PersonRepository : RepositoryBase<Person>, IPersonRepository
    {
        public PersonRepository(IContext context) : base(context)
        {
        }
    }
}
