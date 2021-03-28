using AppCore.Records.Bases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AppCore.DataAccess.Bases
{
    public abstract class RepositoryBase<TEntity> : IDisposable where TEntity : RecordBase, new()
    {
        private readonly DbContext _db;

        protected RepositoryBase(DbContext db)
        {
            _db = db;
        }
        public virtual IQueryable<TEntity> GetEntityQuery(params string[] entititesToInlude)
        {
            try
            {
                var query = _db.Set<TEntity>().AsQueryable();
                foreach (var entityToInclude in entititesToInlude)
                {
                    query = query.Include(entityToInclude);
                }
                return query;
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public virtual IQueryable<TEntity> GetEntityQuery(Expression<Func<TEntity, bool>> predicate, params string[] entititesToInlude)
        {
            try
            {
                var query = GetEntityQuery(entititesToInlude);
                return query.Where(predicate);
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public virtual void Add(TEntity entity,bool saveChanges = true)
        {
            try
            {
                _db.Set<TEntity>().Add(entity);
                if (saveChanges)
                    SaveChanges();
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public virtual void Update(TEntity entity, bool saveChanges = true)
        {
            try
            {
                _db.Set<TEntity>().Update(entity);
                if (saveChanges)
                    SaveChanges();
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public virtual void Delete(TEntity entity, bool saveChanges = true)
        {
            try
            {
                _db.Set<TEntity>().Remove(entity);
                if (saveChanges)
                    SaveChanges();
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public virtual void Delete(int id, bool saveChanges = true)
        {
            try
            {
                var entity = GetEntityQuery(e => e.Id == id).SingleOrDefault();
                Delete(entity, saveChanges);
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public virtual void Delete(string quid, bool saveChanges = true)
        {
            try
            {
                var entity = GetEntityQuery(e => e.Guid == quid).SingleOrDefault();
                Delete(entity, saveChanges);
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }

        public virtual int SaveChanges()
        {
            try
            {
               return _db.SaveChanges();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        #region Dispose
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            _db?.Dispose();
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

}

