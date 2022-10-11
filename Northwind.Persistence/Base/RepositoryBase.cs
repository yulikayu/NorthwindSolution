﻿using Microsoft.EntityFrameworkCore;
using Northwind.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Persistence.Base
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        //Buat manual dan buat konstruktor
        protected NorthwindContext _dbContext;

        protected RepositoryBase(NorthwindContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(T entity) => _dbContext.Set<T>().Add(entity);
        

        public void Delete(T entity) => _dbContext.Set<T>().Add(entity);
        

        public IQueryable<T> FindAll(bool trackChanges) =>
          !trackChanges ? _dbContext.Set<T>().AsNoTracking() : _dbContext.Set<T>();


        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
            !trackChanges ? _dbContext.Set<T>().Where(expression).AsNoTracking() :
             _dbContext.Set<T>().Where(expression);

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }
    }
}
