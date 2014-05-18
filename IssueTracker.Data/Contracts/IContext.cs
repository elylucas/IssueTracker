using System.Data.Entity;

namespace IssueTracker.Data.Contracts
{
    public interface IContext
    {
        //IDbSet<Team> Teams { get; }
        //IDbSet<Person> Persons { get; }
        IDbSet<T> Set<T>() where T : class;
        int SaveChanges();
        void SetModified(object entity);
        void SetAdd(object entity);
        void SetDeleted(object entity);
        void ExecuteSqlCommand(string sql);
        EntityState GetEntityState(object entity);
    }
}
