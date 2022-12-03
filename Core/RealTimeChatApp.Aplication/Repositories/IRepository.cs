﻿using RealTimeChatApp.Domain.Entities.Common;
using System.Linq.Expressions;

namespace RealTimeChatApp.Application.Repositories;


public interface IRepository<TEntity, in TPrimaryKey> : IDisposable where TEntity : BaseEntity<TPrimaryKey>
{
    IQueryable<TEntity> GetAll();
    IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors);
    Task<List<TEntity>> GetAllList();
    Task<List<TEntity>> GetAllListIncluding(params Expression<Func<TEntity, object>>[] propertySelectors);
    Task<TEntity> GetFirst(Expression<Func<TEntity, bool>> predicate);
    IQueryable<TEntity> GetBy(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> Get(TPrimaryKey id);
    Task<TEntity> Insert(TEntity entity);
    Task<TEntity> Update(TEntity entity);
    Task Delete(TPrimaryKey id);
    Task Delete(TEntity entity);
    Task DeleteWhere(Expression<Func<TEntity, bool>> predicate);
    Task<int> Count();
    Task<int> Count(Expression<Func<TEntity, bool>> predicate);
    Task<bool> Any(Expression<Func<TEntity, bool>> predicate);
    Task<bool> All(Expression<Func<TEntity, bool>> predicate);
    Task Commit(CancellationToken cancellationToken = default);
}



