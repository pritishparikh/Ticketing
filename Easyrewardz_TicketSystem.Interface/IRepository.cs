using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>retuns record with matched id</returns>
        T GetByID(object id);

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>returns inserted id</returns>
        int Insert(T entity);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>returns upadated id</returns>
        int Update(T entity);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>returns deleted id</returns>
        int Delete(T entity);

        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns>returns all records</returns>
        IEnumerable<T> SelectAll();

        /// <summary>
        /// Executes the SQL command.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>returns result of sql command</returns>
        int ExecuteSqlCommand(string query, params object[] parameters);

        /// <summary>
        /// Executes the with store procedure.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns> returns result of stored procedure</returns>
        IQueryable<T> ExecWithStoreProcedure(string query, params object[] parameters);

        /// <summary>
        /// Executes the stored procedure list.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>returns result of stored procedures</returns>
        IQueryable<TEntity> ExecuteStoredProcedureList<TEntity>(string sql, params object[] parameters)
            where TEntity : class;
    }
}
