using Skillhouse.HRportal.Common;
using Skillhouse.HRportal.Entity;
using Microsoft.EntityFrameworkCore;
using Skillhouse.HRportal.Repository.Contracts;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Skillhouse.HRportal.Core.Models;
using Skillhouse.HRportal.Core.Constraints;

namespace Skillhouse.HRportal.Repository.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationContext _applicationContext;
        protected readonly HRportalDbContext _hRportalDbContext;
        protected readonly DbSet<T> dbset;

        public Repository(HRportalDbContext hRportalDbContext, ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
            _hRportalDbContext = hRportalDbContext;
            if (_hRportalDbContext == null)
            {
                throw new Exception("Invalid HRportal db context.");
            }

            this.dbset = _hRportalDbContext.Set<T>();
        }

        /// <summary>
        /// The Add an entity
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        public virtual void Add(T entity, bool suppressDivIdMapping = false, bool suppressCreatedOnMapping = false)
        {
            if (entity is IAuditable auditable)
            {
                auditable.CreatedBy = auditable.UpdatedBy = _applicationContext.UserId;
                auditable.UpdatedOn = DateTime.UtcNow;
            }

            if (!suppressCreatedOnMapping && entity is IAuditable createdOn)
            {
                createdOn.CreatedOn = DateTime.UtcNow;
            }

            this.dbset.Add(entity);
        }

        /// <summary>
        /// The Update an entity
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        public virtual void Update(T entity)
        {
            if (entity is IAuditable auditable)
            {
                auditable.UpdatedBy = _applicationContext.UserId;
                auditable.UpdatedOn = DateTime.UtcNow;
            }

            var entry = _hRportalDbContext.Entry(entity);
            this.dbset.Attach(entity);
            entry.State = EntityState.Modified;
        }

        /// <summary>
        /// The Delete an entity
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        public virtual void Delete(T entity)
        {
            var entry = _hRportalDbContext.Entry(entity);
            entry.State = EntityState.Deleted;
        }

        /// <summary>
        /// To get the entities by provided condition.
        /// </summary>
        /// <param name="predicate">The conditions to filter with.</param>
        /// <returns>The list of entities</returns>GetAllAsync
        public List<T> GetBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            return this.GetByAsync(predicate, includes).Result;
        }

        public List<VM> GetByWithSelection<VM>(Expression<Func<T, bool>> predicate, Expression<Func<T, VM>> selectExpression, params Expression<Func<T, object>>[] includes)
        {
            return this.GetByWithSelectionAsync(predicate, selectExpression, includes).Result;
        }

        /// <summary>
        /// To get the entities by provided condition.
        /// </summary>
        /// <param name="predicate">The conditions to filter with.</param>
        /// <returns>The list of entities</returns>
        public PagedResultModel<T> GetBy(Expression<Func<T, bool>> predicate, FilterModel filterModel, params Expression<Func<T, object>>[] includes)
        {
            return this.GetFilteredRecords(filterModel, predicate, x => x, includes).Result;
        }

        public PagedResultModel<VM> GetByWithSelection<VM>(Expression<Func<T, bool>> predicate, FilterModel filterModel, Expression<Func<T, VM>> selectExpression, params Expression<Func<T, object>>[] includes)
        {
            return this.GetFilteredRecords(filterModel, predicate, selectExpression, includes).Result;
        }

        /// <summary>
        /// To get the entities by provided condition asynchronously.
        /// </summary>
        /// <param name="predicate">The conditions to filter with.</param>
        /// <param name="includes">The 0 or more navigation properties to include for EF eager loading.</param>
        /// <returns>The list of entities</returns>
        public async Task<List<T>> GetByAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            if (includes != null)
            {
                return await this.Include(this.dbset.Where(this.ApplyFilters(predicate)), includes).ToListAsync().ConfigureAwait(false);
            }

            return await this.dbset.Where(this.ApplyFilters(predicate)).ToListAsync().ConfigureAwait(false);
        }

        public async Task<List<VM>> GetByWithSelectionAsync<VM>(Expression<Func<T, bool>> predicate, Expression<Func<T, VM>> selectExpression, params Expression<Func<T, object>>[] includes)
        {
            if (includes != null)
            {
                return await this.Include(this.dbset.Where(this.ApplyFilters(predicate)), includes).Select(selectExpression).ToListAsync().ConfigureAwait(false);
            }

            return await this.dbset.Where(this.ApplyFilters(predicate)).Select(selectExpression).ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="filterModel"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public async Task<PagedResultModel<T>> GetByAsync(Expression<Func<T, bool>> predicate, FilterModel filterModel, params Expression<Func<T, object>>[] includes)
        {
            return await this.GetFilteredRecords(filterModel, predicate, x => x, includes);
        }

        public async Task<PagedResultModel<VM>> GetByWithSelectionAsync<VM>(Expression<Func<T, bool>> predicate, FilterModel filterModel, Expression<Func<T, VM>> selectExpression, params Expression<Func<T, object>>[] includes)
        {
            return await this.GetFilteredRecords(filterModel, predicate, selectExpression, includes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="filterModel"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public async Task<List<T>> GetByWithoutPaginationAsync(Expression<Func<T, bool>> predicate, FilterModel filterModel, params Expression<Func<T, object>>[] includes)
        {
            return await this.GetFilteredRecordsWithoutPagination(filterModel, predicate, includes);
        }

        /// <summary>
        /// To get all entities.
        /// </summary>
        /// <param name="includes">The 0 or more navigation properties to include for EF eager loading.</param>
        /// <returns>The list of entities</returns>
        public virtual List<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            return this.GetAllAsync(includes).Result;
        }

        public virtual List<VM> GetAllWithSelection<VM>(Expression<Func<T, VM>> selectExpression, params Expression<Func<T, object>>[] includes)
        {
            return this.GetAllWithSelectionAsync(selectExpression, includes).Result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public virtual PagedResultModel<T> GetAll(FilterModel filterModel, params Expression<Func<T, object>>[] includes)
        {
            return this.GetAllAsync(filterModel, includes).Result;
        }

        /// <summary>
        /// To get all entities asynchronously.
        /// </summary>
        /// <param name="includes">The 0 or more navigation properties to include for EF eager loading.</param>
        /// <returns>The list of entities</returns>
        public virtual async Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            if (includes != null)
            {
                return await this.Include(this.dbset.Where(this.ApplyFilters()), includes).ToListAsync().ConfigureAwait(false);
            }

            return await this.dbset.Where(this.ApplyFilters()).ToListAsync().ConfigureAwait(false);
        }

        public virtual async Task<List<VM>> GetAllWithSelectionAsync<VM>(Expression<Func<T, VM>> selectExpression, params Expression<Func<T, object>>[] includes)
        {
            if (includes != null)
            {
                return await this.Include(this.dbset.Where(this.ApplyFilters()), includes).Select(selectExpression).ToListAsync().ConfigureAwait(false);
            }

            return await this.dbset.Where(this.ApplyFilters()).Select(selectExpression).ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public virtual async Task<PagedResultModel<T>> GetAllAsync(FilterModel filterModel, params Expression<Func<T, object>>[] includes)
        {
            return await this.GetFilteredRecords(filterModel, x => true, x => x, includes);
        }

        public virtual async Task<PagedResultModel<VM>> GetAllWithSelectionAsync<VM>(FilterModel filterModel, Expression<Func<T, VM>> selectExpression, params Expression<Func<T, object>>[] includes)
        {
            return await this.GetFilteredRecords(filterModel, x => true, selectExpression, includes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public virtual async Task<List<T>> GetAllWithoutPaginationAsync(FilterModel filterModel, params Expression<Func<T, object>>[] includes)
        {
            return await this.GetFilteredRecordsWithoutPagination(filterModel, x => true, includes);
        }

        /// <summary>
        /// To determine the existance of an entity by the provided condition.
        /// </summary>
        /// <param name="predicate">The condition to filter with.</param>
        /// <returns>Either true or false as per the match.</returns>
        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return this.AnyAsync(predicate).Result;
        }

        /// <summary>
        /// To determine the existance of an entity by the provided condition asynchronously.
        /// </summary>
        /// <param name="predicate">The condition to filter with.</param>
        /// <returns>Either true or false as per the match.</returns>
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await this.dbset.AnyAsync(this.ApplyFilters(predicate)).ConfigureAwait(false);
        }

        /// <summary>
        /// The Dispose
        /// </summary>
        public virtual void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public Expression<Func<T, object>> GetSortExpression(FilterModel filterModel)
        {
            var entityType = typeof(T);
            if (filterModel.SortMember.Contains('.'))
            {
                var param = Expression.Parameter(entityType, "instance");
                string[] childProperties = filterModel.SortMember.Split('.');

                Expression memberExpression = param;

                foreach (var childProperty in childProperties)
                {
                    if (memberExpression.Type.GetInterfaces().Any(s => s.Namespace == "System.Collections.Generic" && (s.Name.StartsWith("IEnumerable"))))
                    {
                        //query.OrderBy(x => x.LeadCourses.Where(y => y.IsDeleted == false).OrderBy(y => y.Course.Name).Select(y => y.Course.Name).FirstOrDefault());
                        var innerType = memberExpression.Type.GetGenericArguments()[0];

                        ParameterExpression innerParamExpression = Expression.Parameter(innerType, "innerInstance");

                        // 1 = 1, used as a fallback when soft deletion condition is not applicable.
                        Expression innerCondition = Expression.Equal(Expression.Constant(true), Expression.Constant(true));

                        if (!_applicationContext.SuppressSoftDeleteFilters)
                        {
                            // Apply the soft deletion filter if applicable
                            if (typeof(ISoftDelete).IsAssignableFrom(innerType))
                            {
                                // innerInstance.IsDeleted
                                MemberExpression innerMemberIsDeletedExpression = Expression.Property(innerParamExpression, "IsDeleted");

                                // innerInstance.IsDeleted == false
                                innerCondition = Expression.Equal(innerMemberIsDeletedExpression, Expression.Constant(false));
                            }
                        }

                        // innerInstance => innerInstance.IsDeleted == false
                        var innerPredicate = (Expression?)typeof(Expression).GetMethods().First(m =>
                               m.Name == "Lambda" &&
                               m.GetGenericArguments().Length == 1 &&
                               m.GetParameters().Length == 2 &&
                               m.GetParameters()[0].ParameterType == typeof(Expression) &&
                               m.GetParameters()[1].ParameterType == typeof(ParameterExpression[]))
                              .MakeGenericMethod(typeof(Func<,>).MakeGenericType(innerType, typeof(bool)))
                                .Invoke(null, new object[] { innerCondition, new ParameterExpression[] { innerParamExpression } });

                        // instance.Member.Where(innerInstance => innerInstance.IsDeleted == false)
                        var whereCall = Expression.Call(typeof(Enumerable),
                            "Where",
                            memberExpression.Type.GetGenericArguments(),
                            memberExpression,
                            innerPredicate);

                        // Course
                        MemberExpression innerMemberExpression = Expression.Property(innerParamExpression, childProperty);

                        // Course.Name
                        innerMemberExpression = Expression.Property(innerMemberExpression, childProperties[childProperties.Length - 1]);

                        var innerMemberType = innerMemberExpression.Type;

                        // innerInstance => innerInstance.Course.Name
                        var projectionExpression = (Expression?)typeof(Expression).GetMethods().First(m =>
                               m.Name == "Lambda" &&
                               m.GetGenericArguments().Length == 1 &&
                               m.GetParameters().Length == 2 &&
                               m.GetParameters()[0].ParameterType == typeof(Expression) &&
                               m.GetParameters()[1].ParameterType == typeof(ParameterExpression[]))
                              .MakeGenericMethod(typeof(Func<,>).MakeGenericType(innerType, innerMemberType))
                            .Invoke(null, new object[] { innerMemberExpression, new ParameterExpression[] { innerParamExpression } });

                        // instance.LeadCourses.Where(innerInstance => (innerInstance.IsDeleted == False)).OrderBy(innerInstance => innerInstance.Course.Name)
                        var orderByCall = Expression.Call(typeof(Enumerable),
                            filterModel.SortDescending ? "OrderByDescending" : "OrderBy",
                            new Type[] { innerType, innerMemberType },
                            whereCall,
                            projectionExpression);

                        // instance.LeadCourses.Where(innerInstance => (innerInstance.IsDeleted == False)).OrderBy(innerInstance => innerInstance.Course.Name).Select(innerInstance => innerInstance.Course.Name)
                        var projectionCall = Expression.Call(typeof(Enumerable),
                            "Select",
                            new Type[] { innerType, innerMemberType },
                            orderByCall,
                            projectionExpression);

                        // instance.LeadCourses.Where(innerInstance => (innerInstance.IsDeleted == False)).OrderBy(innerInstance => innerInstance.Course.Name).Select(innerInstance => innerInstance.Course.Name).FirstOrDefault()
                        var orderByFieldSelect = Expression.Call(typeof(Enumerable),
                            "FirstOrDefault",
                            new Type[] { innerMemberType },
                            projectionCall);

                        // instance = instance.LeadCourses.Where(innerInstance => (innerInstance.IsDeleted == False)).OrderBy(innerInstance => innerInstance.Course.Name).Select(innerInstance => innerInstance.Course.Name).FirstOrDefault()
                        var finalSortExpression = Expression.Lambda<Func<T, object>>(orderByFieldSelect, param);

                        return finalSortExpression;
                    }
                    else
                    {
                        memberExpression = Expression.Property(memberExpression, childProperty);
                    }
                }

                var sortExpression = Expression.Lambda<Func<T, object>>(memberExpression, param);
                return sortExpression;
            }
            else if (entityType.GetProperties().Any(x => x.Name.ToLower() == filterModel.SortMember.ToLower() && x.CanRead))
            {
                PropertyInfo? propertyInfo = entityType.GetProperty(filterModel.SortMember);
                ParameterExpression parameterExpression = Expression.Parameter(entityType, "instance");
                MemberExpression memberExpression = Expression.Property(parameterExpression, filterModel.SortMember);
                Expression conversion = Expression.Convert(memberExpression, typeof(object));
                var result = Expression.Lambda<Func<T, object>>(conversion, parameterExpression);
                return result;
            }

            return null;
        }

        public Expression<Func<T, bool>> FilterExpression(List<FilterOption> filters)
        {
            Expression<Func<T, bool>> filterExpression = x => true;

            foreach (var filter in filters)
            {
                var entityType = typeof(T);
                if (filter.Property.Contains('.'))
                {
                    var parameterExpression = Expression.Parameter(entityType, "instance");
                    string[] childProperties = filter.Property.Split('.');

                    Expression memberExpression = parameterExpression;
                    bool isComposed = false;

                    foreach (var childProperty in childProperties)
                    {
                        if (memberExpression.Type.GetInterfaces().Any(s => s.Namespace == "System.Collections.Generic" && (s.Name.StartsWith("IEnumerable"))))
                        {
                            var innerType = memberExpression.Type.GetGenericArguments()[0];

                            ParameterExpression innerParamExpression = Expression.Parameter(innerType, "innerInstance");

                            // innerInstance.Property (Ex: leadCourse.Course)
                            MemberExpression innerMemberExpression = Expression.Property(innerParamExpression, childProperty);

                            // innerInstance.Property.ChildProperty (Ex: leadCourse.Course.Name)
                            innerMemberExpression = Expression.Property(innerMemberExpression, childProperties[childProperties.Length - 1]);

                            // innerInstance.Property.ChildProperty == filter.Value (Ex: leadCourse.Course.Name == filter.Value)
                            // NOTE: Using eq for nq. Because the inversion is done for nq at 'Any' expression level below.
                            var innerCondition = this.GetFilterCondition(filter.Value, innerMemberExpression, filter.Operator == "nq" ? "eq" : filter.Operator);

                            if (!_applicationContext.SuppressSoftDeleteFilters)
                            {
                                // Apply the soft deletion filter if applicable
                                if (typeof(ISoftDelete).IsAssignableFrom(innerType))
                                {
                                    // innerInstance.IsDeleted
                                    MemberExpression innerMemberIsDeletedExpression = Expression.Property(innerParamExpression, "IsDeleted");

                                    // innerInstance.IsDeleted && innerInstance.Property.ChildProperty == filter.Value
                                    // (Ex: leadCourse.IsDeleted == false && leadCourse.Course.Name == filter.Value)
                                    innerCondition = Expression.AndAlso(Expression.Equal(innerMemberIsDeletedExpression, Expression.Constant(false)), innerCondition);
                                }
                            }

                            // innerInstance => innerInstance.IsDeleted && innerInstance.Property.ChildProperty == filter.Value
                            // (Ex: leadCourse => leadCourse.IsDeleted == false && leadCourse.Course.Name == filter.Value)
                            var innerPredicate = (Expression?)typeof(Expression).GetMethods().First(m =>
                               m.Name == "Lambda" &&
                               m.GetGenericArguments().Length == 1 &&
                               m.GetParameters().Length == 2 &&
                               m.GetParameters()[0].ParameterType == typeof(Expression) &&
                               m.GetParameters()[1].ParameterType == typeof(ParameterExpression[]))
                              .MakeGenericMethod(typeof(Func<,>).MakeGenericType(innerType, typeof(bool)))
                                .Invoke(null, new object[] { innerCondition, new ParameterExpression[] { innerParamExpression } });

                            // instance.LeadCourses.Any(innerInstance => innerInstance.IsDeleted == false && innerInstance.Property.ChildProperty == filter.Value)
                            // Ex: lead.LeadCourses.Any(leadCourse => leadCourse.IsDeleted && leadCourse.Course.Name == filter.Value)
                            var anyCall = Expression.Call(typeof(Enumerable), "Any", memberExpression.Type.GetGenericArguments(), memberExpression, innerPredicate);

                            // instance => instance.LeadCourses.Any(innerInstance => innerInstance.IsDeleted == false && innerInstance.Property.ChildProperty == filter.Value)
                            // Ex: lead => lead.LeadCourses.Any(leadCourse => leadCourse.IsDeleted && leadCourse.Course.Name == filter.Value)
                            var finalExpression = Expression.Lambda<Func<T, bool>>(filter.Operator == "nq" ? Expression.Not(anyCall) : anyCall, parameterExpression);

                            // combine with existing filter expression
                            filterExpression = filterExpression.And(finalExpression);
                            isComposed = true;
                            break;
                        }
                        else
                        {
                            // Compose n level chiled (Ex: x.property.subproperty and so on in interations);
                            memberExpression = Expression.Property(memberExpression, childProperty);
                        }
                    }

                    if (!isComposed)
                    {
                        var condition = this.GetFilterCondition(filter.Value, memberExpression, filter.Operator);
                        var predicate = Expression.Lambda<Func<T, bool>>(condition, parameterExpression);
                        filterExpression = filterExpression.And(predicate);
                    }
                }
                else if (entityType.GetProperties().Any(x => x.Name.ToLower() == filter.Property.ToLower() && x.CanRead))
                {
                    ParameterExpression parameterExpression = Expression.Parameter(entityType, "instance");
                    MemberExpression memberExpression = Expression.Property(parameterExpression, filter.Property);

                    var condition = this.GetFilterCondition(filter.Value, memberExpression, filter.Operator);
                    var predicate = Expression.Lambda<Func<T, bool>>(condition, parameterExpression);
                    filterExpression = filterExpression.And(predicate);
                }
            }

            return filterExpression;
        }

        public async Task<int> SaveAsync()
        {
            return await _hRportalDbContext.SaveAsync();
        }

        /// <summary>
        /// To apply the filtes to provided expression.
        /// </summary>
        /// <param name="expression">The expression to which the filter needs to be applied.</param>
        /// <returns>Filtered expression.</returns>
        protected Expression<Func<T, bool>> ApplyFilters(Expression<Func<T, bool>> expression = null)
        {
            // By default consider all.
            Expression<Func<T, bool>> filter = x => true;

            if (!_applicationContext.SuppressSoftDeleteFilters)
            {
                // Apply the soft deletion filter if applicable
                if (typeof(ISoftDelete).IsAssignableFrom(typeof(T)))
                {
                    filter = x => !((ISoftDelete)x).IsDeleted;
                }
            }
            return expression == null ? filter : expression.And(filter);
        }

        private IQueryable<T> Include(IQueryable<T> query, params Expression<Func<T, object>>[] includes)
        {
            foreach (var include in includes)
            {
                query = this.IncludeProperty(query, include);
            }

            return query;
        }

        private IQueryable<T> IncludeProperty<TProperty>(IQueryable<T> query, Expression<Func<T, TProperty>> include)
        {
            return query.Include(include);
        }

        private async Task<PagedResultModel<VM>> GetFilteredRecords<VM>(FilterModel filterModel, Expression<Func<T, bool>> predicate, Expression<Func<T, VM>> selectExpression, params Expression<Func<T, object>>[] includes)
        {
            PagedResultModel<VM> pagedResult = new PagedResultModel<VM>();

            var skip = (filterModel.PageNumber) * filterModel.PageSize;

            Expression<Func<T, bool>> searchFilterExpression = null;
            var generalFilterExpression = this.ApplyFilters(predicate);
            var sortExpression = GetSortExpression(filterModel);
            if (filterModel.Filters?.Count > 0)
            {
                searchFilterExpression = FilterExpression(filterModel.Filters);
                pagedResult.TotalRecords = this.dbset.Where(generalFilterExpression).Where(searchFilterExpression).Count();
            }
            else
            {
                pagedResult.TotalRecords = (this.dbset.Where(generalFilterExpression)).Count();
            }

            pagedResult.TotalPages = ((pagedResult.TotalRecords + filterModel.PageSize - 1) / filterModel.PageSize);

            IQueryable<T> source;

            if (filterModel.Filters?.Count > 0)
            {
                source = this.dbset.Where(generalFilterExpression).Where(searchFilterExpression);
            }
            else
            {
                source = this.dbset.Where(generalFilterExpression);
            }

            if (includes != null)
            {
                source = this.Include(source, includes);
            }
            //TODO:Need To Revisit
            var leadInfo = filterModel.Filters.Select(x => x).Where(x => x.Property == "NoOfLeads").FirstOrDefault();
            if (filterModel.SortDescending)
            {
                pagedResult.Records = await source
                  .OrderByDescending(sortExpression)
                  .Skip(skip).Take(filterModel.PageSize).Select(selectExpression).ToListAsync().ConfigureAwait(false);

                if (leadInfo != null)
                {
                    pagedResult.Records = await source.OrderByDescending(sortExpression).Select(selectExpression).ToListAsync().ConfigureAwait(false);
                }
            }
            else
            {
                pagedResult.Records = await source
                   .OrderBy(sortExpression)
                   .Skip(skip).Take(filterModel.PageSize).Select(selectExpression).ToListAsync().ConfigureAwait(false);
                if (leadInfo != null)
                {
                    pagedResult.Records = await source.OrderBy(sortExpression).Select(selectExpression).ToListAsync().ConfigureAwait(false);
                }
            }
            return pagedResult;
        }

        private async Task<List<T>> GetFilteredRecordsWithoutPagination(FilterModel filterModel, Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var generalFilterExpression = this.ApplyFilters(predicate);
            var sortExpression = GetSortExpression(filterModel);

            IQueryable<T> source;

            if (filterModel.Filters?.Count > 0)
            {
                var searchFilterExpression = FilterExpression(filterModel.Filters);
                source = this.dbset.Where(generalFilterExpression).Where(searchFilterExpression);
            }
            else
            {
                source = this.dbset.Where(generalFilterExpression);
            }

            if (includes != null)
            {
                source = this.Include(source, includes);
            }

            List<T> result = null;

            if (filterModel.SortDescending)
            {
                result = await source
                   .OrderByDescending(sortExpression)
                   .ToListAsync().ConfigureAwait(false);
            }
            else
            {
                result = await source
                    .OrderBy(sortExpression)
                    .ToListAsync().ConfigureAwait(false);
            }

            return result;
        }

        private Expression GetFilterCondition(string filterValue, Expression memberExpression, string filterOperator)
        {
            Type memberType = memberExpression.Type;
            bool isNullableType = false;
            if (memberExpression.Type.IsGenericType && memberExpression.Type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                memberType = Nullable.GetUnderlyingType(memberType);
                isNullableType = true;
            }
            Expression constantOrValueExpression;
            //When the date field is empty string we are creating nullable expresion
            if (memberExpression.Type == typeof(DateTime?))
            {
                constantOrValueExpression = Expression.Constant(null, memberExpression.Type);
            }
            else
            {
                constantOrValueExpression = Expression.Constant(Convert.ChangeType(filterValue, memberType));
            }
            if (isNullableType && memberExpression.Type != typeof(DateTime?))
            {
                // Create value expression when member is of nullable type.
                constantOrValueExpression = Expression.Convert(constantOrValueExpression, memberExpression.Type);
            }
            Expression expression;
            if (filterOperator == null && (memberExpression.Type == typeof(int) || memberExpression.Type == typeof(int?) || memberExpression.Type == typeof(bool) || memberExpression.Type == typeof(bool?)))
            {
                filterOperator = Constants.Operators.Equal;
            }

            if (filterOperator != null)
            {
                if (memberExpression.Type == typeof(DateTime) || memberExpression.Type == typeof(DateTime?))
                {
                    expression = isNullableType
                        ? this.GetNullableDateFilterExpression(memberExpression, filterOperator, constantOrValueExpression, filterValue)
                        : this.GetDateFilterExpression(memberExpression, filterOperator, constantOrValueExpression);
                }
                else
                {
                    MethodInfo containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                    switch (filterOperator)
                    {
                        case Constants.Operators.Equal:
                            expression = Expression.Equal(memberExpression, constantOrValueExpression);
                            break;
                        case Constants.Operators.LessThan:
                            expression = Expression.LessThan(memberExpression, constantOrValueExpression);
                            break;
                        case Constants.Operators.NotEqual:
                            expression = Expression.NotEqual(memberExpression, constantOrValueExpression);
                            break;
                        case Constants.Operators.Contains:
                            expression = isNullableType ? Expression.Equal(memberExpression, constantOrValueExpression) : Expression.Call(memberExpression, containsMethod, constantOrValueExpression);
                            break;
                        case Constants.Operators.NotContains:
                            expression = isNullableType ? Expression.NotEqual(memberExpression, constantOrValueExpression) : Expression.Not(Expression.Call(memberExpression, containsMethod, constantOrValueExpression));
                            break;
                        case Constants.Operators.GreaterThan:
                            expression = Expression.GreaterThan(memberExpression, constantOrValueExpression);
                            break;
                        case Constants.Operators.LessThanOrEquals:
                            expression = Expression.LessThanOrEqual(memberExpression, constantOrValueExpression);
                            break;
                        case Constants.Operators.GreaterThanOrEquals:
                            expression = Expression.GreaterThanOrEqual(memberExpression, constantOrValueExpression);
                            break;
                        default:
                            throw new ApplicationException("Invalid operator!");
                    }
                }
            }
            else
            {
                MethodInfo containsMethodInfo = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                expression = Expression.Call(memberExpression, containsMethodInfo, constantOrValueExpression);
            }

            return expression;
        }

        private Expression GetDateFilterExpression(Expression memberExpression, string filterOperator, Expression constantOrValueExpression)
        {
            Expression expression;
            var datePropertyAccess = Expression.MakeMemberAccess(memberExpression, typeof(DateTime).GetProperty("Date"));
            switch (filterOperator)
            {
                case Constants.Operators.Equal:
                    expression = Expression.Equal(datePropertyAccess, constantOrValueExpression);
                    break;
                case Constants.Operators.NotEqual:
                    expression = Expression.NotEqual(datePropertyAccess, constantOrValueExpression);
                    break;
                case Constants.Operators.LessThan:
                    expression = Expression.LessThan(datePropertyAccess, constantOrValueExpression);
                    break;
                case Constants.Operators.GreaterThan:
                    expression = Expression.GreaterThan(datePropertyAccess, constantOrValueExpression);
                    break;
                case Constants.Operators.LessThanOrEquals:
                    expression = Expression.LessThanOrEqual(datePropertyAccess, constantOrValueExpression);
                    break;
                case Constants.Operators.GreaterThanOrEquals:
                    expression = Expression.GreaterThanOrEqual(datePropertyAccess, constantOrValueExpression);
                    break;
                default:
                    throw new ApplicationException("Invalid operator!");
            }
            return expression;
        }

        private Expression GetNullableDateFilterExpression(Expression memberExpression, string filterOperator, Expression constantExpression, string filterValue)
        {
            Expression expression;
            var valueProperty = typeof(DateTime?).GetProperty("Value");
            var dateProperty = typeof(DateTime).GetProperty("Date");
            var valuePropertyAccess = Expression.MakeMemberAccess(memberExpression, valueProperty);
            var datePropertyAccess = filterValue == string.Empty ? memberExpression : Expression.MakeMemberAccess(valuePropertyAccess, dateProperty);
            switch (filterOperator)
            {
                case Constants.Operators.Equal:
                    expression = Expression.Equal(datePropertyAccess, constantExpression);
                    break;
                case Constants.Operators.NotEqual:
                    expression = Expression.NotEqual(datePropertyAccess, constantExpression);
                    break;
                case Constants.Operators.LessThan:
                    expression = Expression.LessThan(datePropertyAccess, constantExpression);
                    break;
                case Constants.Operators.GreaterThan:
                    expression = Expression.GreaterThan(datePropertyAccess, constantExpression);
                    break;
                case Constants.Operators.LessThanOrEquals:
                    expression = Expression.LessThanOrEqual(datePropertyAccess, constantExpression);
                    break;
                case Constants.Operators.GreaterThanOrEquals:
                    expression = Expression.GreaterThanOrEqual(datePropertyAccess, constantExpression);
                    break;
                default:
                    throw new ApplicationException("Invalid operator!");
            }
            return expression;
        }

        /// <summary>
        /// The Dispose
        /// </summary>
        /// <param name="dispose">The Dispose</param>
        private void Dispose(bool dispose)
        {
            if (dispose)
            {
                // Nothing to dispose here.
            }
        }
    }
}
