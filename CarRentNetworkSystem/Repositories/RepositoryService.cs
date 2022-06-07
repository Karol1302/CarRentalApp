using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ATHCarRentNetworkSystem.Data;

namespace ATHCarRentNetworkSystem.Repositories
{
    public interface IEntity<T>
    {
        public T Id { get; set; }
    }

    public interface IRepositoryService<T> where T : IEntity<int>
    {
        IQueryable<T> GetAllRecords();
        T GetSingle(int id);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        ServiceResult Add(T entity);
        ServiceResult Delete(T entity);
        ServiceResult Edit(T entity);
        ServiceResult Save();
        bool Exists(int id);
    }

    public class RepositoryService<T> : IRepositoryService<T> where T : class, IEntity<int>
    {
        protected DbContext _context;
        protected DbSet<T> _set;

        public RepositoryService(ApplicationDbContext context)
        {
            _context = context;
            _set = _context.Set<T>();
        }


        public ServiceResult Add(T entity)
        {
            ServiceResult result = new();
            try
            {
                _set.Add(entity);
                result = Save();
            }
            catch (Exception ex)
            {
                result.Result = ServiceResultStatus.Error;
                result.Messages.Add(ex.Message);
            }
            return result;
        }

        public ServiceResult Delete(T entity)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                _set.Remove(entity);
                result = Save();
            }
            catch (Exception e)
            {
                result.Result = ServiceResultStatus.Error;
                result.Messages.Add(e.Message);
            }
            return result;
        }

        public ServiceResult Edit(T entity)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                result = Save();
            }
            catch (Exception e)
            {
                result.Result = ServiceResultStatus.Error;
                result.Messages.Add(e.Message);
            }
            return result;
        }

        public bool Exists(int id) => _set.Any(r => r.Id == id);

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _set.Where(predicate);
            return query;
        }

        public IQueryable<T> GetAllRecords()
        {
            return _set;
        }

        public T GetSingle(int id)
        {
            var result = _set.FirstOrDefault(r => r.Id == id);
            return result;
        }

        public ServiceResult Save()
        {
            ServiceResult result = new ServiceResult();
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                result.Result = ServiceResultStatus.Error;
                result.Messages.Add(e.Message);
            }
            return result;
        }
    }
}
