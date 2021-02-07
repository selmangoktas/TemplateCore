using TemplateCore.DAL.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace TemplateCore.BL.Repositories
{
    public class WebRepository<T> where T : class
    {
        private readonly WebContext db;
        private DbSet<T> entities;

        public WebRepository(WebContext _db)
        {
            db = _db;
            entities = _db.Set<T>();
        }

        public T GetBy(Expression<Func<T, bool>> expression)
        {
            return entities.FirstOrDefault(expression);
        }

        public IQueryable<T> GetAll()
        {
            return entities;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> expression)
        {
            return entities.Where(expression);
        }

        public void Add(T entity)
        {
            db.Add(entity);
            db.SaveChanges();
        }

        public void AddRange(IQueryable<T> entities)
        {
            db.AddRange(entities);
            db.SaveChanges();
        }

        public void Remove(T entity)
        {
            db.Remove(entity);
            db.SaveChanges();
        }

        public void RemoveRange(IQueryable<T> entities)
        {
            db.RemoveRange(entities);
            db.SaveChanges();
        }

        public void Update(T entity, params Expression<Func<T, object>>[] expressions)
        {
            if (expressions.Any()) foreach (Expression<Func<T, object>> expression in expressions) db.Entry(entity).Property(expression).IsModified = true;
            else db.Update(entity);
            db.SaveChanges();
        }

        //public void Update(T entity)
        //{
        //    db.Update(entity);
        //    db.SaveChanges();
        //}
    }
}

