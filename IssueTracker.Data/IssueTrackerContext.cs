using System.Data.Entity;
using IssueTracker.Data.Contracts;
using IssueTracker.Entities;

namespace IssueTracker.Data
{
    public class IssueTrackerContext : DbContext, IContext
    {
        public IDbSet<Person> People { get; set; }
        public IDbSet<Issue> Issues { get; set; }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }
        public void SetModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }

        public void SetAdd(object entity)
        {
            Entry(entity).State = EntityState.Added;
        }

        public void SetDeleted(object entity)
        {
            Entry(entity).State = EntityState.Deleted;
        }

        public void ExecuteSqlCommand(string sql)
        {
            base.Database.ExecuteSqlCommand(sql);
        }

        public EntityState GetEntityState(object entity)
        {
            return base.Entry(entity).State;
        }

        public IssueTrackerContext()
        {
            base.Configuration.LazyLoadingEnabled = false;
        }
    }
}
