using ATHCarRentNetworkSystem.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ATHCarRentNetworkSystem.Repositories
{
    public interface IStringRepositoryService<T> where T : IEntity<string>
    {
        IQueryable<T> GetAllRecords();
        T GetSingle(string id);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        ServiceResult Add(T entity);
        ServiceResult Delete(T entity);
        ServiceResult Edit(T entity);
        ServiceResult Save();
        //bool Exists(string id);
    }
    public class StringRepositoryService<T> : IStringRepositoryService<T> where T: class, IEntity<string>
    {
        protected DbContext _context;
        protected DbSet<T> _set;

        public StringRepositoryService(ApplicationDbContext context)
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

        public bool Exists(string id) => _set.Any(r => r.Id == id);

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _set.Where(predicate);
            return query;
        }

        public IQueryable<T> GetAllRecords()
        {
            return _set;
        }

        public T GetSingle(string id)
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

