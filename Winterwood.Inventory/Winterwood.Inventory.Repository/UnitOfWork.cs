using System;
using System.Data.Entity;
using Winterwood.Inventory.Entity;

namespace Winterwood.Inventory.Repository
{
    public class UnitOfWork : IDisposable
    {
        #region Private Members

        /// <summary>
        /// Holds the object context
        /// </summary>
        private readonly Microsoft.EntityFrameworkCore.DbContext _context;

        private bool _isDisposed;

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        internal winterwooddbContext Context
        {
            get
            {
                return _context as winterwooddbContext;
            }
        }

        #endregion Private Members

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public UnitOfWork()
        {
            _context = new winterwooddbContext();

        }

        #endregion Constructors

        #region Internal Methods

        /// <summary>
        /// Creates the object set.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        internal Microsoft.EntityFrameworkCore.DbSet<T> CreateDbSet<T>() where T : class
        {
            return _context.Set<T>();
        }

        #endregion Internal Methods

        #region Public Methods

        /// <summary>
        /// Saves the changes.
        /// </summary>
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _context.Dispose();
            _isDisposed = true;
        }

        #endregion Public Methods

        /// <summary>
        /// Gets a value indicating whether this instance is disposed.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is disposed; otherwise, <c>false</c>.
        /// </value>
        public bool IsDisposed
        {
            get { return this._isDisposed; }
        }
    }
}
