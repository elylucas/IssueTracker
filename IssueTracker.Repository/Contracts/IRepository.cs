using System.Collections.Generic;

namespace IssueTracker.Repository.Contracts
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(params string[] includes);
        T GetById(int id, string[] includes = null);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(int id);

    }
}
