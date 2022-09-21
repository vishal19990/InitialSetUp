using Skillhouse.HRportal.Core.Models;
using Skillhouse.HRportal.Entity;
using System.Linq.Expressions;

namespace Skillhouse.HRportal.Repository.Contracts
{
    public interface IRepository<T> : IDisposable where T : BaseEntity
    {
        /// <summary>
        /// To get all entities.
        /// </summary>
        /// <param name="includes">The 0 or more navigation properties to include for EF eager loading.</param>
        /// <returns>The list of entities</returns>
        List<T> GetAll(params Expression<Func<T, object>>[] includes);

        PagedResultModel<T> GetAll(FilterModel filterModel, params Expression<Func<T, object>>[] includes);

        List<VM> GetByWithSelection<VM>(Expression<Func<T, bool>> predicate, Expression<Func<T, VM>> selectExpression, params Expression<Func<T, object>>[] includes);

        Task<List<VM>> GetByWithSelectionAsync<VM>(Expression<Func<T, bool>> predicate, Expression<Func<T, VM>> selectExpression, params Expression<Func<T, object>>[] includes);

        Task<List<VM>> GetAllWithSelectionAsync<VM>(Expression<Func<T, VM>> selectExpression, params Expression<Func<T, object>>[] includes);

        List<VM> GetAllWithSelection<VM>(Expression<Func<T, VM>> selectExpression, params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// To get all entities asynchronously.
        /// </summary>
        /// <param name="includes">The 0 or more navigation properties to include for EF eager loading.</param>
        /// <returns>The list of entities</returns>
        Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);

        Task<PagedResultModel<T>> GetAllAsync(FilterModel filterModel, params Expression<Func<T, object>>[] includes);

        Task<List<T>> GetAllWithoutPaginationAsync(FilterModel filterModel, params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// To get the entities by provided condition.
        /// </summary>
        /// <param name="predicate">The conditions to filter with.</param>
        /// <returns>The list of entities</returns>
        List<T> GetBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

        PagedResultModel<T> GetBy(Expression<Func<T, bool>> predicate, FilterModel filterModel, params Expression<Func<T, object>>[] includes);

        Task<List<T>> GetByAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

        Task<PagedResultModel<T>> GetByAsync(Expression<Func<T, bool>> predicate, FilterModel filterModel, params Expression<Func<T, object>>[] includes);

        Task<List<T>> GetByWithoutPaginationAsync(Expression<Func<T, bool>> predicate, FilterModel filterModel, params Expression<Func<T, object>>[] includes);

        PagedResultModel<VM> GetByWithSelection<VM>(Expression<Func<T, bool>> predicate, FilterModel filterModel, Expression<Func<T, VM>> selectExpression, params Expression<Func<T, object>>[] includes);

        Task<PagedResultModel<VM>> GetAllWithSelectionAsync<VM>(FilterModel filterModel, Expression<Func<T, VM>> selectExpression, params Expression<Func<T, object>>[] includes);

        Task<PagedResultModel<VM>> GetByWithSelectionAsync<VM>(Expression<Func<T, bool>> predicate, FilterModel filterModel, Expression<Func<T, VM>> selectExpression, params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// To determine the existance of an entity by the provided condition.
        /// </summary>
        /// <param name="predicate">The condition to filter with.</param>
        /// <returns>Either true or false as per the match.</returns>
        bool Any(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// To determine the existance of an entity by the provided condition asynchronously.
        /// </summary>
        /// <param name="predicate">The condition to filter with.</param>
        /// <returns>Either true or false as per the match.</returns>
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// The Add an entity
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <param name="suppressOrgIdMapping">
        void Add(T entity, bool suppressOrgIdMapping = false, bool suppressCreatedOnMapping = false);

        /// <summary>
        /// The Update an entity
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        void Update(T entity);

        /// <summary>
        /// The Delete an entity
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        void Delete(T entity);

        //Sorting the records by asc/Desc
        Expression<Func<T, object>> GetSortExpression(FilterModel filterModel);

        Task<int> SaveAsync();
    }
}
