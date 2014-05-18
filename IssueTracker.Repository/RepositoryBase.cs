using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using IssueTracker.Data.Contracts;
using IssueTracker.Entities;
using IssueTracker.Entities.Exceptions;
using IssueTracker.Repository.Contracts;

namespace IssueTracker.Repository
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : EntityBase
    {
        protected readonly IContext _context;
        protected readonly IDbSet<T> _dbSet;

        public RepositoryBase(IContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet;
        }

        public virtual IEnumerable<T> GetAll(params string[] includes)
        {
            var query = _dbSet.AsQueryable();
            includes.ToList().ForEach(x => query = query.Include(x));

            return query;
        }

        public virtual T GetById(int id, string[] includes = null)
        {
            var query = _dbSet.AsQueryable();
            if (includes != null)
            {
                includes.ToList().ForEach(x => query = query.Include(x));
            }

            var entity = query.SingleOrDefault(x => x.Id == id);
            if (entity == null)
            {
                throw new EntityNotFoundException("Not Found");
            }
            return entity;
        }

        public virtual void Add(T entity)
        {
            EntityState entityState = _context.GetEntityState(entity);
            if (entityState != EntityState.Detached)
            {
                _context.SetAdd(entity);
            }
            else
            {
                _dbSet.Add(entity);
            }
            try
            {
                SaveChanges();
            }
            catch (DbUpdateException ex)
            {
            }

        }

        public virtual void Update(T entity)
        {
            EntityState entityState = _context.GetEntityState(entity);
            if (entityState == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _context.SetModified(entity);

            try
            {
                SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
        }

        public virtual void Delete(T entity)
        {
            EntityState entityState = _context.GetEntityState(entity);
            if (entityState != EntityState.Deleted)
            {
                _context.SetDeleted(entity);
            }
            else
            {
                _dbSet.Attach(entity);
                _dbSet.Remove(entity);
            }

            try
            {
                SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }

        }

        public virtual void Delete(int id)
        {
            var entity = GetById(id);
            if (entity == null) return;
            Delete(entity);
        }

        protected void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
