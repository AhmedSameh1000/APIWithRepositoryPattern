using Microsoft.EntityFrameworkCore;
using Repositorypatern.UnitOfWork.Core.Consts;
using Repositorypatern.UnitOfWork.Core.Interfaces;
using Repositorypatern.UnitOfWork.EF.Data;
using System.Linq.Expressions;

namespace Repositorypatern.UnitOfWork.EF.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public BaseRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public T Add(T Entity)
        {
            _applicationDbContext.Set<T>().Add(Entity);
            _applicationDbContext.SaveChanges();
            return Entity;
        }

        public IEnumerable<T> AddRange(IEnumerable<T> Entities)
        {
            _applicationDbContext.Set<T>().AddRange(Entities);
            _applicationDbContext.SaveChanges();
            return Entities;
        }

        public T Find(Expression<Func<T, bool>> match, string[] Includes = null)
        {
            IQueryable<T> query = _applicationDbContext.Set<T>();
            if (Includes is not null)
            {
                foreach (var Include in Includes)
                {
                    query = query.Include(Include);
                }
            }
            return query.SingleOrDefault(match);
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> match, string[] Includes = null)
        {
            IQueryable<T> query = _applicationDbContext.Set<T>();
            if (Includes is not null)
            {
                foreach (var Include in Includes)
                {
                    query = query.Include(Include);
                }
            }
            return query.Where(match).ToList();
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> match, int take, int skip)
        {
            return _applicationDbContext.Set<T>().Where(match).Skip(skip).Take(take).ToList();
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> match, int? take, int? skip, Expression<Func<T, object>> orderBy = null, string OrderByDirection = "ASC")
        {
            IQueryable<T> query = _applicationDbContext.Set<T>().Where(match);
            if (take.HasValue)
                query = query.Take(take.Value);
            if (skip.HasValue)
                query = query.Skip(skip.Value);
            if (orderBy is not null)
            {
                if (OrderByDirection == OrderBy.Ascending)
                {
                    query = query.OrderBy(orderBy);
                }
                else
                {
                    query = query.OrderByDescending(orderBy);
                }
            }
            return query.ToList();
        }

        public IEnumerable<T> GetAll()
        {
            return _applicationDbContext.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _applicationDbContext.Set<T>().Find(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _applicationDbContext.Set<T>().FindAsync(id);
        }
    }
}