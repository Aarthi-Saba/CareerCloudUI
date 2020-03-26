using CareerCloud.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CareerCloud.EntityFrameworkDataAccess
{
    public class EFGenericRepository<T> : IDataRepository<T> where T : class
    {
        private readonly CareerCloudContext _currentcontext;

        public EFGenericRepository()
        {
            DbContextOptions<CareerCloudContext> options = new DbContextOptions<CareerCloudContext>();
            _currentcontext = new CareerCloudContext(options);
        }
        public void Add(params T[] items)
        {
            foreach(T poco in items)
            {
                _currentcontext.Entry(poco).State = EntityState.Added;
            }
            _currentcontext.SaveChanges();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }
        private IQueryable<T> GetRelatedRecord(params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> joinQuery = _currentcontext.Set<T>();
            foreach(Expression<Func<T, object>> property in navigationProperties)
            {
                joinQuery = joinQuery.Include<T, object>(property);
            }
            return joinQuery;
        }
        public IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            //IQueryable<T> joinQuery = _currentcontext.Set<T>();
            //foreach(Expression<Func<T,object>> property in navigationProperties)
            //{
            //    joinQuery = joinQuery.Include<T, object>(property);
            //}
            IQueryable<T> dbQuery=GetRelatedRecord(navigationProperties);
            return dbQuery.ToList<T>();
        }

        public IList<T> GetList(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            //    IQueryable<T> joinQuery = _currentcontext.Set<T>();
            //    foreach (Expression<Func<T, object>> property in navigationProperties)
            //    {
            //        joinQuery = joinQuery.Include<T, object>(property);
            //    }
            IQueryable<T> dbQuery = GetRelatedRecord(navigationProperties);
            return dbQuery.Where(where).ToList<T>();
        }

        public T GetSingle(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            //IQueryable<T> joinQuery = _currentcontext.Set<T>();
            //foreach (Expression<Func<T, object>> property in navigationProperties)
            //{
            //    joinQuery = joinQuery.Include<T, object>(property);
            //}
            IQueryable<T> dbQuery = GetRelatedRecord(navigationProperties);
            return dbQuery.Where(where).FirstOrDefault();
        }

        public void Remove(params T[] items)
        {
            foreach(T poco in items)
            {
                _currentcontext.Entry(poco).State = EntityState.Deleted;
            }
            _currentcontext.SaveChanges();
        }

        public void Update(params T[] items)
        {
            foreach(T poco in items)
            {
                _currentcontext.Entry(poco).State = EntityState.Modified;
            }
            _currentcontext.SaveChanges();
        }
    }
}
