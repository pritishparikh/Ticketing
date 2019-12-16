using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Easyrewardz_TicketSystem.DBContext;
using Easyrewardz_TicketSystem.Interface;


namespace Easyrewardz_TicketSystem.Services
{
    public sealed class RepositoryService<T> : IRepository<T>
        where T : class
    {
        #region Context 
        /// <summary>
        /// Context 
        /// </summary>
        private readonly ETSContext dbContext;
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="IRepository{T}"/> class.
        /// </summary>
        /// <param name="context"></param>
        public RepositoryService(ETSContext context)
        {
            this.dbContext = context;
        }

        #endregion

        #region Method defination 

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>returns record with matched id</returns>
        public T GetByID(object id)
        {
            return this.dbContext.Set<T>().Find(id);
        }


        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>returns inserted id</returns>
        /// <exception cref="ArgumentNullException">entity</exception>
        public int Insert(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }

                this.dbContext.Set<T>().Add(entity);
                return this.dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return 0;
            }
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>returns updated id</returns>
        /// <exception cref="ArgumentNullException">entity</exception>
        public int Update(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }

                this.dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                ////Entities.Update(entity);    ---- use this if above modified state changes not working

                return this.dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return 0;
            }
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>returns deleted id</returns>
        /// <exception cref="ArgumentNullException">entity</exception>
        public int Delete(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }

                this.dbContext.Set<T>().Remove(entity);

                return this.dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return 0;
            }
        }

        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns>returns list of records</returns>
        public IEnumerable<T> SelectAll()
        {
            return this.dbContext.Set<T>().AsEnumerable<T>();
        }


        /// <summary>
        /// Executes the SQL command.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>returns results of sql commands</returns>
        public int ExecuteSqlCommand(string query, params object[] parameters)
        {
            return this.dbContext.Database.ExecuteSqlCommand(query, parameters);
        }


        /// <summary>
        /// Executes the with store procedure.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>returns result of stored procedure</returns>
        public IQueryable<T> ExecWithStoreProcedure(string query, params object[] parameters)
        {
            return this.dbContext.Set<T>().FromSql(query, parameters);
        }

        /// <summary>
        /// Executes the stored procedure list.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>returns result of stored procedures</returns>
        public IQueryable<TEntity> ExecuteStoredProcedureList<TEntity>(string sql, params object[] parameters)
            where TEntity : class
            => this.dbContext.Set<TEntity>().FromSql(sql, parameters);



        #endregion

    }
}
