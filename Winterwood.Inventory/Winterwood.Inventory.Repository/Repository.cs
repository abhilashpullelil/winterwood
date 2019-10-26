using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Winterwood.Inventory.Repository.Interfaces;

namespace Winterwood.Inventory.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        #region Private Members

        /// <summary>
        /// Holds object set
        /// </summary>
        private readonly Microsoft.EntityFrameworkCore.DbSet<T> _dbSet;

        /// <summary>
        /// Object context
        /// </summary>
        private readonly UnitOfWork _dbContext;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="dbContext">The db context.</param>
        public Repository(UnitOfWork dbContext)
        {
            _dbContext = dbContext as UnitOfWork;
            _dbSet = _dbContext.CreateDbSet<T>();
        }

        #endregion

        #region IConnection Implementation

        /// <summary>
        /// Make as queryable.
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> AsQueryable()
        {
            return _dbSet;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        /// <summary>
        /// Find the entity for the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        /// <summary>
        /// Get single entity for specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        public T Single(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.SingleOrDefault(predicate);
        }



        /// <summary>
        /// Get first entity for the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public T Add(T entity)
        {
            try
            {

                //_dbContext.Context.Entry(entity).State = System.Data.Entity.EntityState.Added;
                _dbSet.Add(entity);
                _dbContext.SaveChanges();
                return entity;
            }
            catch (Exception dbEx)
            {
                _dbSet.Remove(entity);
                return null;
            }
        }

        /// <summary>
        /// Attaches the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Attach(T entity)
        {
            _dbSet.Attach(entity);
        }

        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        /// <summary>
        /// Removes entities for the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        public void Remove(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> records = from x in _dbSet.Where(predicate) select x;
            foreach (T record in records)
            {
                _dbSet.Remove(record);
            }
        }

        /// <summary>
        /// Softs the delete.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void SoftDelete(T entity)
        {
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Update(T entity)
        {
            //_dbContext.Context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Gets the item count for the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        public int GetCount(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Local.AsQueryable().Count(predicate);
        }

        /// <summary>
        /// Iterate each items and add to the context
        /// </summary>
        /// <param name="items"></param>
        public void AddItems(List<T> items)
        {
            foreach (var entity in items)
            {
                _dbSet.Add(entity);
            }

            _dbContext.SaveChanges();
        }

        #endregion
    }
}
