using System;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Web;
using DataContext;
using Models;

namespace Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Base
    {
        private readonly SimpleLibraryDbContext context;		
        private DbContextTransaction transaction = null;
        public GenericRepository()
        {
            context = new SimpleLibraryDbContext();
            
        }

        private IDbSet<T> _entities;
        private IDbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = context.Set<T>();
                return _entities;
            }
        }

        public IQueryable<T> GetAll(bool archived, bool versioned)
        {
            return this.Entities.Where(i => (versioned || i.Versioned == versioned) &&
                (archived || i.Archived == archived));
        }

        public IQueryable<T> GetAll(bool archived, bool versioned, int pageSize, int page)
        {
            int skip = pageSize * (page - 1);
            var list = this.Entities.Where(i => i.Archived == archived && i.Versioned == versioned)
                        .OrderBy(i => i.Id)
                        .Skip(skip)
                        .Take(pageSize);
            return list;
        }

        public IQueryable<T> GetAllActive()
        {
            return this.Entities.Where(i => i.Versioned == false && i.Archived == false && i.Deleted == false);
        }

        public IQueryable<T> GetAllActive(int pageSize, int page)
        {
            int skip = pageSize * (page - 1);
            var list = this.Entities.Where(i => i.Versioned == false && i.Archived == false && i.Deleted == false)
                        .OrderBy(i => i.Id).Skip(skip).Take(pageSize);
            return list;
        }

        public T GetById(object id, bool detach = false)
        {
            var record = this.Entities.Find(id);

            if (detach)
                context.Entry(record).State = EntityState.Detached;

            return record;
        }

        public void Insert(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

                entity.CreatedBy = "Rokive";

            this.Entities.Add(entity);
            transaction = context.Database.BeginTransaction();
        }

        public void Insert(IEnumerable<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entity");

            foreach (var item in entities)
            {
                    item.CreatedBy = "Rokive";

                this.Entities.Add(item);
            }
            transaction = context.Database.BeginTransaction();
        }

        public void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            entity.LastModifiedDate = DateTime.UtcNow;

            //context.Entry(entity).State = EntityState.Modified;
            transaction = context.Database.BeginTransaction();
        }

        public void Update(IEnumerable<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entity");

            foreach (var item in entities)
            {
                item.LastModifiedDate = DateTime.UtcNow;

                //context.Entry(item).State = EntityState.Modified;
            }
            transaction = context.Database.BeginTransaction();

        }

        public void Version(T entity, T currentEntity = null)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            if (currentEntity == null)
                currentEntity = GetById(entity.Id);

            if (currentEntity != null)
            {
                if (currentEntity.Versioned || currentEntity.Archived)
                    throw new Exception("Archived or versioned data cannot be editted.");

                currentEntity.Versioned = true;
                context.Entry(currentEntity).State = EntityState.Modified;
                entity.CreatedBy = currentEntity.CreatedBy;
                entity.CreateDate = currentEntity.CreateDate;
            }

            entity.LastModifiedDate = DateTime.UtcNow;

            this.Entities.Add(entity);
            transaction = context.Database.BeginTransaction();
        }

        public void Archive(object id)
        {
            var entity = this.Entities.Find(id);

            if (entity == null)
                throw new ArgumentNullException("entity");

            entity.Archived = true;
            entity.LastModifiedDate = DateTime.UtcNow;

            context.Entry(entity).State = EntityState.Modified;
            transaction = context.Database.BeginTransaction();
        }

        public void Delete(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            this.Entities.Remove(entity);
            transaction = context.Database.BeginTransaction();
        }

        public void Delete(int id)
        {
            var entity = this.Entities.Find(id);
            this.Entities.Remove(entity);
            transaction = context.Database.BeginTransaction();
        }

        public int Count(bool archived, bool versioned)
        {
            return this.Entities.Where(i => i.Archived == archived && i.Versioned == versioned).Count();
        }

        public virtual IQueryable<T> Table
        {
            get
            {
                return this.Entities;
            }
        }
        public IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters)
            where TEntity : Base, new()
        {

            bool hasOutputParameters = false;
            if (parameters != null)
            {
                foreach (var p in parameters)
                {
                    var outputP = p as DbParameter;
                    if (outputP == null)
                        continue;

                    if (outputP.Direction == ParameterDirection.InputOutput ||
                        outputP.Direction == ParameterDirection.Output)
                        hasOutputParameters = true;
                }
            }



            var context = ((IObjectContextAdapter)(this)).ObjectContext;
            if (!hasOutputParameters)
            {
                var result = this.context.Database.SqlQuery<TEntity>(commandText, parameters).ToList();
                return result;
            }
            else
            {
                var connection = this.context.Database.Connection;
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = commandText;
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                        foreach (var p in parameters)
                            cmd.Parameters.Add(p);

                    var reader = cmd.ExecuteReader();
                    var result = context.Translate<TEntity>(reader).ToList();
                    reader.Close();
                    return result;
                }

            }
        }

        public IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            return this.context.Database.SqlQuery<TElement>(sql, parameters);
        }

        public int ExecuteSqlCommand(string sql, int? timeout = null, params object[] parameters)
        {
            int? previousTimeout = null;
            if (timeout.HasValue)
            {
                //store previous timeout
                previousTimeout = ((IObjectContextAdapter)this).ObjectContext.CommandTimeout;
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = timeout;
            }

            var result = this.context.Database.ExecuteSqlCommand(sql, parameters);

            if (timeout.HasValue)
            {
                //Set previous timeout back
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = previousTimeout;
            }

            //return result
            return result;
        }

        public int GetSequence()
        {
            return context.GetNextSequenceValue();
        }
        public void Save()
        {
            
                context.SaveChanges();
                transaction.Commit();
                transaction = context.Database.BeginTransaction();
            
        }
        public void RollBack()
        {
            transaction.Rollback();
            transaction = context.Database.BeginTransaction();
        }
        public void Dispose()
        {
            transaction.Dispose();
            context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}