using Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Repositories
{
    public interface IGenericRepository<T> where T : Base
    {
        IQueryable<T> GetAll(bool archived, bool versioned);
        IQueryable<T> GetAll(bool archived, bool versioned, int pageSize, int pageIndex);
        IQueryable<T> GetAllActive();
        IQueryable<T> GetAllActive(int pageSize, int pageIndex);
        T GetById(object id, bool detach = false);
        void Insert(T entity);
        void Insert(IEnumerable<T> entities);
        void Update(T entity);
        void Update(IEnumerable<T> entities);
        void Version(T entities, T currentEntity = null);
        void Archive(object id);
        void Delete(T entity);
        void Delete(int id);
        int Count(bool archived, bool versioned);
        IQueryable<T> Table { get; }
        IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters)
                                                                                            where TEntity : Base, new();

        IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters);
        int ExecuteSqlCommand(string sql, int? timeout = null, params object[] parameters);
        int GetSequence();
        void Save();
        void RollBack();
        void Dispose();
    }
}
